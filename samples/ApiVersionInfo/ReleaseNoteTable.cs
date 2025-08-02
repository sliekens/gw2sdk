using System.Globalization;

using GuildWars2.Metadata;

using Spectre.Console;
using Spectre.Console.Rendering;

namespace ApiVersionInfo;

internal sealed class ReleaseNoteTable : IRenderable
{
    private readonly Table table = new Table()
        .AddColumn("Change")
        .AddColumn("Description")
        .MinimalBorder();

    public Measurement Measure(RenderOptions options, int maxWidth)
    {
        return ((IRenderable)table).Measure(options, maxWidth);
    }

    public IEnumerable<Segment> Render(RenderOptions options, int maxWidth)
    {
        return ((IRenderable)table).Render(options, maxWidth);
    }

    public void AddRow(Schema schemaVersion)
    {
        var formatted = DateTimeOffset.Parse(schemaVersion.Version, CultureInfo.InvariantCulture)
            .ToString("D", CultureInfo.CurrentCulture);
        table.AddRow(formatted.EscapeMarkup(), schemaVersion.Description.EscapeMarkup());
    }
}
