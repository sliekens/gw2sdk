using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK;
using GW2SDK.Http;
using GW2SDK.Items;
using GW2SDK.Json;
using GW2SDK.Recipes;
using Spectre.Console;

namespace MostVersatileMaterials
{
    internal class Program
    {
        static Program()
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en");
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en");
        }

        private static async Task Main(string[] args)
        {
            using var http = new HttpClient(new SocketsHttpHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip
                },
                true);

            http.UseGuildWars2();
            http.UseLanguage(Language.English);
            http.UseSchemaVersion(SchemaVersion.Recommended);

            var recipesService = new RecipeService(http, new RecipeReader(), MissingMemberBehavior.Undefined);
            var itemsService = new ItemService(http, new ItemReader(), MissingMemberBehavior.Undefined);

            const int top = 600;
            var recipes = await Progress()
                .StartAsync(async ctx =>
                {
                    var recipesProgress =
                        ctx.AddTask("Fetching recipes", new ProgressTaskSettings { AutoStart = false });
                    var ingredientsProgress =
                        ctx.AddTask("Fetching ingredients", new ProgressTaskSettings { AutoStart = false });

                    var craftable = await GetRecipes(recipesService, recipesProgress);

                    var groupedByIngredient = craftable.SelectMany(recipe =>
                            recipe.Ingredients.Select(ingredient => (Ingredient: ingredient.ItemId, Recipe: recipe)))
                        .ToLookup(grouping => grouping.Ingredient, grouping => grouping.Recipe);

                    var mostCommon = groupedByIngredient.OrderByDescending(grouping => grouping.Count())
                        .Take(top)
                        .ToList();

                    var ingredientIds = mostCommon.Select(grouping => grouping.Key)
                        .ToList();

                    var ingredients = await GetItems(ingredientIds, itemsService, ingredientsProgress);

                    return (Ingredients: ingredients, Craftable: groupedByIngredient);
                });

            do
            {
                AnsiConsole.Clear();

                var choice = AnsiConsole.Prompt(new SelectionPrompt<Item>()
                    .Title("Pick an ingredient to see the available recipes")
                    .MoreChoicesText("Scroll down for less commonly used ingredients")
                    .AddChoices(recipes.Ingredients)
                    .UseConverter(item => item.Name)
                    .PageSize(20));

                await using var ingredientIcon = await http.GetStreamAsync(choice.Icon!);
                var choiceTable = new Table().AddColumn("Icon")
                    .AddColumn("Ingredient")
                    .AddColumn("Description");

                choiceTable.AddRow(new CanvasImage(ingredientIcon).MaxWidth(32),
                    new Markup(choice.Name.EscapeMarkup()),
                    new Markup(choice.Description.EscapeMarkup()));

                AnsiConsole.Render(choiceTable);

                var outputs = await Progress()
                    .StartAsync(async ctx =>
                    {
                        var itemIds = recipes.Craftable[choice.Id]
                            .Select(recipe => recipe.OutputItemId)
                            .ToList();
                        return await GetItems(itemIds, itemsService, ctx.AddTask("Fetching output items"));
                    });

                var recipesTable = new Table().AddColumn("Recipe")
                    .AddColumn("Description");

                foreach (var recipe in outputs)
                {
                    recipesTable.AddRow(recipe.Name.EscapeMarkup(), recipe.Description.EscapeMarkup());
                }

                AnsiConsole.Render(recipesTable);
            } while (AnsiConsole.Confirm("Do you want to choose again?"));
        }

        private static Progress Progress() =>
            AnsiConsole.Progress()
                .Columns(new TaskDescriptionColumn(),
                    new ProgressBarColumn(),
                    new PercentageColumn(),
                    new RemainingTimeColumn(),
                    new SpinnerColumn());

        private static async Task<List<Recipe>> GetRecipes(RecipeService recipesService, ProgressTask progress)
        {
            progress.StartTask();

            var page = await recipesService.GetRecipesByPage(0, 200);

            var context = page.Context;
            progress.MaxValue(context.ResultTotal)
                .Value(context.ResultCount);

            var recipes = new List<Recipe>(page.Values) { Capacity = context.ResultTotal };
            var next = context.Next;
            while (!next.IsEmpty)
            {
                page = await recipesService.GetRecipesByPage(next);
                recipes.AddRange(page.Values);

                progress.Increment(context.ResultCount);
            }

            progress.StopTask();

            return recipes;
        }

        private static async Task<List<Item>> GetItems(
            IReadOnlyCollection<int> itemIds,
            ItemService itemsService,
            ProgressTask progress
        )
        {
            progress.MaxValue(itemIds.Count)
                .StartTask();
            var items = new List<Item>(itemIds.Count);
            foreach (var ids in itemIds.Buffer(200))
            {
                var subset = await itemsService.GetItemsByIds(ids.ToList());
                items.AddRange(subset.Values);
                progress.Increment(subset.Context.ResultCount);
            }

            progress.StopTask();

            return items;
        }
    }
}
