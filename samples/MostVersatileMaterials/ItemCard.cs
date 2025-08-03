using GuildWars2.Items;
using GuildWars2.Markup;

using Spectre.Console;

namespace MostVersatileMaterials;

internal sealed class ItemCard(HttpClient httpClient)
{
    public async Task Show(Item item)
    {
        ArgumentNullException.ThrowIfNull(item);

        await using Stream ingredientIcon = await httpClient.GetStreamAsync(item.IconUrl);
        Table itemTable = new Table().AddColumn("Icon")
            .AddColumn("Ingredient")
            .AddColumn("Description");

        var lexer = new MarkupLexer();
        var parser = new MarkupParser();
        var converter = new SpectreMarkupConverter();
        IEnumerable<MarkupToken> tokens = MarkupLexer.Tokenize(item.Description);
        RootNode syntax = MarkupParser.Parse(tokens);
        var description = SpectreMarkupConverter.Convert(syntax);

        itemTable.AddRow(
            new CanvasImage(ingredientIcon).MaxWidth(32),
            new Markup(item.Name.EscapeMarkup()),
            new Markup(description)
        );

        AnsiConsole.Write(itemTable);
    }
}
