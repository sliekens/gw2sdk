using System;
using System.Collections.Generic;
using GuildWars2.Meta;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace ApiVersionInfo;

internal sealed class ReleaseNoteTable : IRenderable
{
    private readonly Table table = new Table()
        .AddColumn("Change")
        .AddColumn("Description")
        .MinimalBorder();

    public void AddRow(Schema schemaVersion)
    {
        var formatted = DateTimeOffset.Parse(schemaVersion.Version).ToString("D");
        table.AddRow(formatted.EscapeMarkup(), schemaVersion.Description.EscapeMarkup());
    }

    public Measurement Measure(RenderContext context, int maxWidth)
    {
        return ((IRenderable)table).Measure(context, maxWidth);
    }

    public IEnumerable<Segment> Render(RenderContext context, int maxWidth)
    {
        return ((IRenderable)table).Render(context, maxWidth);
    }
}
