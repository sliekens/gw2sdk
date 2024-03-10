using System;
using System.Net.Http;
using GuildWars2;
using MostVersatileMaterials;
using Spectre.Console;

try
{
    using var http = new HttpClient();
    var gw2 = new Gw2Client(http);
    var referenceData = await ReferenceData.Fetch(gw2);

    do
    {
        AnsiConsole.Clear();

        var ingredient = ItemPicker.Prompt(referenceData.Ingredients);

        var card = new ItemCard(http);
        await card.Show(ingredient);

        var recipesTable = new RecipesTable();
        AnsiConsole.Live(recipesTable)
            .Start(
                live =>
                {
                    foreach (var recipe in referenceData.OutputsByIngredient[ingredient.Id])
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
