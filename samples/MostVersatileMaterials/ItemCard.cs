using GuildWars2.Items;
using GuildWars2.Markup;

using Spectre.Console;

namespace MostVersatileMaterials;

internal sealed class ItemCard(HttpClient httpClient)
{
    public async Task Show(Item item)
    {
        ArgumentNullException.ThrowIfNull(item);
        Stream ingredientIcon = await httpClient.GetStreamAsync(item.IconUrl).ConfigureAwait(false);
        await using (ingredientIcon.ConfigureAwait(false))
        {
            Table itemTable = new Table().AddColumn("Icon")
                .AddColumn("Ingredient")
                .AddColumn("Description");

            SpectreMarkupConverter converter = new();
            IEnumerable<MarkupToken> tokens = MarkupLexer.Tokenize(item.Description);
            RootNode syntax = MarkupParser.Parse(tokens);
            string description = SpectreMarkupConverter.Convert(syntax);

            itemTable.AddRow(
                new CanvasImage(ingredientIcon).MaxWidth(32),
                new Markup(item.Name.EscapeMarkup()),
                new Markup(description)
            );

            AnsiConsole.Write(itemTable);
        }
    }
}
