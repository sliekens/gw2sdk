using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using GuildWars2;
using GuildWars2.Hero.Crafting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MostVersatileMaterials;
using MostVersatileMaterials.Logging;
using Spectre.Console;

var builder = Host.CreateApplicationBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddProvider(new AnsiConsoleLoggerProvider());

var gw2ClientBuilder = builder.Services.AddHttpClient<Gw2Client>(
    static client =>
    {
        client.BaseAddress = BaseAddress.DefaultUri;
        client.Timeout =
            Timeout.InfiniteTimeSpan; // Allow step-through debugging without timing out
    }
);

gw2ClientBuilder.AddStandardResilienceHandler();
gw2ClientBuilder.AddStandardHedgingHandler();

var app = builder.Build();
try
{
    var gw2 = app.Services.GetRequiredService<Gw2Client>();
    var referenceData = await AnsiConsole.Progress()
        .Columns(
            new TaskDescriptionColumn(),
            new ProgressBarColumn(),
            new PercentageColumn(),
            new RemainingTimeColumn(),
            new SpinnerColumn()
        )
        .StartAsync(
            async ctx =>
            {
                // Fetch all recipes
                var recipesProgress = ctx.AddTask("Fetching recipes");
                var recipes = await gw2.Hero.Crafting.Recipes
                    .GetRecipesBulk(progress: new ConsoleProgressUpdater(recipesProgress))
                    .Select(result => result.Value)
                    .ToListAsync();

                // Fetch all input items
                var inputItemsProgress = ctx.AddTask("Fetching input items");
                var inputItemIds = (
                    from recipe in recipes
                    from ingredient in recipe.Ingredients
                    where ingredient.Kind == IngredientKind.Item
                    select ingredient.Id).ToHashSet();

                var inputItems = await gw2.Items
                    .GetItemsBulk(
                        inputItemIds,
                        progress: new ConsoleProgressUpdater(inputItemsProgress)
                    )
                    .ValueOnly()
                    .ToDictionaryAsync(item => item.Id);

                // Fetch all output items
                var outputItemsProgress = ctx.AddTask("Fetching output items");
                var outputItemIds = (
                    from recipe in recipes
                    select recipe.OutputItemId).ToHashSet();
                var outputItems = await gw2.Items.GetItemsBulk(
                        outputItemIds,
                        progress: new ConsoleProgressUpdater(outputItemsProgress)
                    )
                    .ValueOnly()
                    .ToDictionaryAsync(item => item.Id);

                // The progress bar doesn't reach 100% because the API doesn't return all output items (yeah, it's pretty broken)
                // anyway, fake it until you make it
                outputItemsProgress.MaxValue(outputItemsProgress.Value);

                return new ReferenceData
                {
                    Recipes = recipes,
                    InputItems = inputItems,
                    OutputItems = outputItems
                };
            }
        );

    var recipesByIngredient = (
        from recipe in referenceData.Recipes
        from ingredient in recipe.Ingredients
        where ingredient.Kind == IngredientKind.Item
        select (ingredient.Id, recipe)).ToLookup(tuple => tuple.Id, tuple => tuple.recipe);

    var itemsSortedByNumberOfRecipes = (
        from recipes in recipesByIngredient
        let count = recipes.Count()
        orderby count descending
        select (referenceData.InputItems[recipes.Key], count)).ToList();

    var outputsByIngredient = (
        from recipes in recipesByIngredient
        from recipe in recipes
        let outputItem = referenceData.OutputItems.GetValueOrDefault(recipe.OutputItemId)
        where outputItem is not null
        select (recipes.Key, outputItem)).ToLookup(
        recipe => recipe.Key,
        recipe => recipe.outputItem
    );

    do
    {
        var ingredient = ItemPicker.Prompt(itemsSortedByNumberOfRecipes);

        using var http = app.Services.GetRequiredService<HttpClient>();
        var card = new ItemCard(http);
        await card.Show(ingredient);

        var recipesTable = new RecipesTable();
        AnsiConsole.Live(recipesTable)
            .Start(
                live =>
                {
                    foreach (var recipe in outputsByIngredient[ingredient.Id])
                    {
                        recipesTable.AddRow(recipe);
                        live.Refresh();
                    }
                }
            );
    } while (AnsiConsole.Confirm("Do you want to choose again?"));
}
catch (Exception exception)
{
    AnsiConsole.WriteException(exception);
}
