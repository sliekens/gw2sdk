using GuildWars2.Items;
using GuildWars2.Markup;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace MostVersatileMaterials;

public class RecipesTable : IRenderable
{
    private static readonly MarkupTextConverter TextConverter = new();

    private readonly Table table = new Table().AddColumn("Recipe").AddColumn("Description");

    public Measurement Measure(RenderOptions options, int maxWidth) =>
        ((IRenderable)table).Measure(options, maxWidth);

    public IEnumerable<Segment> Render(RenderOptions options, int maxWidth) =>
        ((IRenderable)table).Render(options, maxWidth);

    public void AddRow(Item item)
    {
        var lexer = new MarkupLexer();
        var parser = new MarkupParser(lexer.Tokenize(item.Description));
        var description = TextConverter.Convert(parser.Parse());
        table.AddRow(item.Name.EscapeMarkup(), description.EscapeMarkup());
    }
}
