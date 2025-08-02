using GuildWars2.Items;
using GuildWars2.Markup;

using Spectre.Console;
using Spectre.Console.Rendering;

namespace MostVersatileMaterials;

internal class RecipesTable : IRenderable
{
    private readonly Table table = new Table().AddColumn("Recipe").AddColumn("Description");

    public Measurement Measure(RenderOptions options, int maxWidth)
    {
        return ((IRenderable)table).Measure(options, maxWidth);
    }

    public IEnumerable<Segment> Render(RenderOptions options, int maxWidth)
    {
        return ((IRenderable)table).Render(options, maxWidth);
    }

    public void AddRow(Item item)
    {
        ArgumentNullException.ThrowIfNull(item);

        var tokens = MarkupLexer.Tokenize(item.Description);
        var syntax = MarkupParser.Parse(tokens);
        var description = SpectreMarkupConverter.Convert(syntax);
        table.AddRow(item.Name.EscapeMarkup(), description);
    }
}
