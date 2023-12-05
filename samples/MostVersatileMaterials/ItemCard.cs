using System.Net.Http;
using System.Threading.Tasks;
using GuildWars2.Items;
using Spectre.Console;

namespace MostVersatileMaterials;

public class ItemCard(HttpClient httpClient)
{
    public async Task Show(Item item)
    {
        await using var ingredientIcon = await httpClient.GetStreamAsync(item.IconHref!);
        var itemTable = new Table().AddColumn("Icon")
            .AddColumn("Ingredient")
            .AddColumn("Description");

        itemTable.AddRow(
            new CanvasImage(ingredientIcon).MaxWidth(32),
            new Markup(item.Name.EscapeMarkup()),
            new Markup(item.Description.EscapeMarkup())
        );

        AnsiConsole.Write(itemTable);
    }
}
