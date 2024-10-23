using GuildWars2.Items;
using GuildWars2.Markup;
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

        var lexer = new MarkupLexer();
        var parser = new MarkupParser();
        var converter = new SpectreMarkupConverter();
        var tokens = lexer.Tokenize(item.Description);
        var syntax = parser.Parse(tokens);
        var description = converter.Convert(syntax);

        itemTable.AddRow(
            new CanvasImage(ingredientIcon).MaxWidth(32),
            new Markup(item.Name.EscapeMarkup()),
            new Markup(description)
        );

        AnsiConsole.Write(itemTable);
    }
}
