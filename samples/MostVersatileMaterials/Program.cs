using System;
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

            var (ingredients, recipes) = await Progress()
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

                    var ingredientIndex = groupedByIngredient.Select(grouping => grouping.Key)
                        .ToHashSet();

                    var ingredients = await GetItems(ingredientIndex, itemsService, ingredientsProgress);

                    var ingredientsDictionary = ingredients.ToDictionary(item => item.Id);

                    var mostCommon = groupedByIngredient.OrderByDescending(grouping => grouping.Count())
                        .Select(grouping => ingredientsDictionary[grouping.Key])
                        .ToList();

                    return (Ingredients: mostCommon, Craftable: groupedByIngredient);
                });

            do
            {
                AnsiConsole.Clear();

                var choice = AnsiConsole.Prompt(new SelectionPrompt<Item>()
                    .Title("Pick an ingredient to see the available recipes")
                    .MoreChoicesText("Scroll down for less commonly used ingredients")
                    .AddChoices(ingredients)
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
                        var itemIds = recipes[choice.Id]
                            .Select(recipe => recipe.OutputItemId)
                            .ToHashSet();
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
            try
            {
                return await recipesService.GetRecipes(progress: new ProgressTaskAdapter(progress))
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
            IReadOnlySet<int> itemIds,
            ItemService itemsService,
            ProgressTask progress
        )
        {
            var items = new List<Item>(itemIds.Count);

            progress.StartTask();
            await foreach (var item in itemsService.GetItemsByIds(itemIds, progress: new ProgressTaskAdapter(progress)))
            {
                items.Add(item.Value);
            }

            progress.StopTask();

            return items;
        }
    }

    internal class ProgressTaskAdapter : IProgress<ICollectionContext>
    {
        private readonly ProgressTask progressTask;

        public ProgressTaskAdapter(ProgressTask progressTask)
        {
            this.progressTask = progressTask;
        }

        public void Report(ICollectionContext value)
        {
            progressTask.MaxValue(value.ResultTotal);
            progressTask.Increment(value.ResultCount);
        }
    }
}
