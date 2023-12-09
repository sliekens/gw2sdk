using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuildWars2;
using GuildWars2.Hero.Crafting;
using GuildWars2.Hero.Crafting.Recipes;
using GuildWars2.Items;
using Spectre.Console;

namespace MostVersatileMaterials;

public class ReferenceData
{
    public required List<Item> Ingredients { get; init; }

    public required ILookup<int, Item> OutputsByIngredient { get; init; }

    public static async Task<ReferenceData> Fetch(Gw2Client gw2) =>
        await AnsiConsole.Progress()
            .Columns(
                new TaskDescriptionColumn(),
                new ProgressBarColumn(),
                new PercentageColumn(),
                new RemainingTimeColumn(),
                new SpinnerColumn()
            )
            .StartAsync(ctx => FetchImpl(gw2, ctx));

    private static async Task<ReferenceData> FetchImpl(Gw2Client gw2, ProgressContext ctx)
    {
        var recipesProgress = ctx.AddTask(
            "Fetching recipes",
            new ProgressTaskSettings { AutoStart = false }
        );

        var ingredientsProgress = ctx.AddTask(
            "Fetching ingredients",
            new ProgressTaskSettings { AutoStart = false }
        );

        var outputsProgress = ctx.AddTask(
            "Fetching output items",
            new ProgressTaskSettings { AutoStart = false }
        );

        var craftingRecipes = await GetRecipes(gw2.Hero.Crafting.Recipes, recipesProgress);

        var groupedByIngredient = craftingRecipes
            .SelectMany(
                recipe => recipe.Ingredients
                    .Where(ingredient => ingredient.Kind == IngredientKind.Item)
                    .Select(ingredient => (Ingredient: ingredient.Id, Recipe: recipe))
            )
            .ToLookup(grouping => grouping.Ingredient, grouping => grouping.Recipe);

        var ingredientIndex = groupedByIngredient.Select(grouping => grouping.Key).ToHashSet();

        var craftingIngredients = await GetItems(ingredientIndex, gw2.Items, ingredientsProgress);

        var ingredientsById = craftingIngredients.ToDictionary(item => item.Id);

        var mostCommon = groupedByIngredient.OrderByDescending(grouping => grouping.Count())
            .Select(grouping => ingredientsById[grouping.Key])
            .ToList();

        var outputs = craftingRecipes.Select(recipe => recipe.OutputItemId)
            .DistinctBy(itemId => itemId)
            .ToList();
        var outputItems = await GetItems(outputs, gw2.Items, outputsProgress);
        var outputItemsById = outputItems.ToDictionary(item => item.Id);

        ctx.Refresh();

        return new ReferenceData
        {
            Ingredients = mostCommon,
            OutputsByIngredient = groupedByIngredient
                .SelectMany(
                    group => group.Select(
                        recipe => (Ingredient: group.Key, Output: recipe.OutputItemId)
                    )
                )
                .Where(group => outputItemsById.ContainsKey(group.Output))
                .ToLookup(group => group.Ingredient, group => outputItemsById[group.Output])
        };
    }

    private static async Task<List<Recipe>> GetRecipes(
        RecipesClient recipesClient,
        ProgressTask progress
    )
    {
        progress.StartTask();
        try
        {
            return await recipesClient.GetRecipesBulk(progress: new ProgressAdapter(progress))
                .Select(result => result.Value)
                .OrderByDescending(recipe => recipe.Id)
                .ToListAsync();
        }
        finally
        {
            progress.StopTask();
        }
    }

    private static async Task<List<Item>> GetItems(
        IReadOnlyCollection<int> itemIds,
        ItemsClient itemsClient,
        ProgressTask progress
    )
    {
        progress.StartTask();

        try
        {
            return await itemsClient.GetItemsBulk(itemIds, progress: new ProgressAdapter(progress))
                .Select(result => result.Value)
                .ToListAsync();
        }
        finally
        {
            progress.StopTask();
        }
    }

    private static void UpdateProgress(ResultContext ctx, ProgressTask progressTask)
    {
        progressTask.MaxValue(ctx.ResultTotal);
        progressTask.Value(ctx.ResultCount);
    }
}
