using System.Collections.Generic;
using GuildWars2.Items;
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

    public void AddRow(Item item) =>
        table.AddRow(item.Name.EscapeMarkup(), item.Description.EscapeMarkup());
}
