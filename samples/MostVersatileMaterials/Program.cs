using GuildWars2;
using GuildWars2.Hero.Crafting;
using GuildWars2.Hero.Crafting.Recipes;
using GuildWars2.Items;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using MostVersatileMaterials;

using Spectre.Console;

// This example retrieves all item ingredients that are used in crafting,
// sorted by how many recipes require the material
HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

// The console is used for user interactions, not for logging
// Redirect logs to the 'Output' window in Visual Studio
builder.Logging.ClearProviders();
builder.Logging.AddDebug();

IHttpClientBuilder gw2ClientBuilder = builder.Services.AddHttpClient<Gw2Client>(static client =>
    {
        client.BaseAddress = BaseAddress.DefaultUri;
        client.Timeout =
            Timeout.InfiniteTimeSpan; // Allow step-through debugging without timing out
    }
);

gw2ClientBuilder.AddStandardResilienceHandler();

IHost app = builder.Build();
try
{
    Gw2Client gw2 = app.Services.GetRequiredService<Gw2Client>();
    ReferenceData referenceData = await AnsiConsole.Progress()
        .Columns(
            new TaskDescriptionColumn(),
            new ProgressBarColumn(),
            new PercentageColumn(),
            new RemainingTimeColumn(),
            new SpinnerColumn()
        )
        .StartAsync(async ctx =>
            {
                // Fetch all recipes
                ProgressTask recipesProgress = ctx.AddTask("Fetching recipes");
                List<Recipe> recipes = await gw2.Hero.Crafting.Recipes
                    .GetRecipesBulk(progress: new ProgressTaskUpdater(recipesProgress))
                    .Select(result => result.Value)
                    .ToListAsync()
                    .ConfigureAwait(false);

                // Fetch all input items
                ProgressTask inputItemsProgress = ctx.AddTask("Fetching input items");
                HashSet<int> inputItemIds = [.. (
                    from recipe in recipes
                    from ingredient in recipe.Ingredients
                    where ingredient.Kind == IngredientKind.Item
                    select ingredient.Id)];

                Dictionary<int, Item> inputItems = await gw2.Items
                    .GetItemsBulk(
                        inputItemIds,
                        progress: new ProgressTaskUpdater(inputItemsProgress)
                    )
                    .ValueOnly()
                    .ToDictionaryAsync(item => item.Id)
                    .ConfigureAwait(false);

                // Fetch all output items
                ProgressTask outputItemsProgress = ctx.AddTask("Fetching output items");
                HashSet<int> outputItemIds = [.. (
                    from recipe in recipes
                    select recipe.OutputItemId)];
                Dictionary<int, Item> outputItems = await gw2.Items.GetItemsBulk(
                        outputItemIds,
                        progress: new ProgressTaskUpdater(outputItemsProgress)
                    )
                    .ValueOnly()
                    .ToDictionaryAsync(item => item.Id)
                    .ConfigureAwait(false);

                // The progress bar doesn't reach 100% because the API doesn't return all output items (yeah, it's pretty broken)
                // anyway, fake it until you make it
                outputItemsProgress.MaxValue(outputItemsProgress.Value);

                return new ReferenceData
                {
                    Recipes = recipes.AsReadOnly(),
                    InputItems = inputItems,
                    OutputItems = outputItems
                };
            }
        )
        .ConfigureAwait(false);

    ILookup<int, Recipe> recipesByIngredient = (
        from recipe in referenceData.Recipes
        from ingredient in recipe.Ingredients
        where ingredient.Kind == IngredientKind.Item
        select (ingredient.Id, recipe)).ToLookup(tuple => tuple.Id, tuple => tuple.recipe);

    List<(Item, int count)> itemsSortedByNumberOfRecipes = [.. (
        from recipes in recipesByIngredient
        let count = recipes.Count()
        orderby count descending
        select (referenceData.InputItems[recipes.Key], count))];

    ILookup<int, Item> outputsByIngredient = (
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
        Item ingredient = ItemPicker.Prompt(itemsSortedByNumberOfRecipes);

        using HttpClient http = app.Services.GetRequiredService<HttpClient>();
        ItemCard card = new(http);
        await card.Show(ingredient)
        .ConfigureAwait(false);

        RecipesTable recipesTable = new();
        AnsiConsole.Live(recipesTable)
            .Start(live =>
                {
                    foreach (Item recipe in outputsByIngredient[ingredient.Id])
                    {
                        recipesTable.AddRow(recipe);
                        live.Refresh();
                    }
                }
            );
    } while (await AnsiConsole.ConfirmAsync("Do you want to choose again?")
    .ConfigureAwait(false));
}
#pragma warning disable CA1031 // Do not catch general exception types
catch (Exception exception)
{
    AnsiConsole.WriteException(exception);
}
#pragma warning restore CA1031 // Do not catch general exception types
