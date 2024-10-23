using GuildWars2.Items;
using GuildWars2.Markup;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace MostVersatileMaterials;

public class RecipesTable : IRenderable
{
    private readonly Table table = new Table().AddColumn("Recipe").AddColumn("Description");

    public Measurement Measure(RenderOptions options, int maxWidth) =>
        ((IRenderable)table).Measure(options, maxWidth);

    public IEnumerable<Segment> Render(RenderOptions options, int maxWidth) =>
        ((IRenderable)table).Render(options, maxWidth);

    public void AddRow(Item item)
    {
        var lexer = new MarkupLexer();
        var parser = new MarkupParser();
        var converter = new MarkupTextConverter();
        var tokens = lexer.Tokenize(item.Description);
        var syntax = parser.Parse(tokens);
        var description = converter.Convert(syntax);
        table.AddRow(item.Name.EscapeMarkup(), description.EscapeMarkup());
    }
}
