using System.Collections.Generic;
using GuildWars2.Meta;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace ApiVersionInfo;

internal sealed class RouteTable : IRenderable
{
    private readonly Options options;

    private readonly Table table = new Table()
        .AddColumn("Route")
        .AddColumn("Authorization")
        .AddColumn("Localization")
        .MinimalBorder();

    public RouteTable(Options options)
    {
        this.options = options;
    }

    public void AddRoute(Route route, IReadOnlyCollection<string> languages)
    {
        if (route.RequiresAuthorization && !options.ShowAuthorized)
        {
            return;
        }

        if (!route.Active && !options.ShowDisabled)
        {
            return;
        }

        var pathTemplate = route.Active switch
        {
            false => ":no_entry: [dim bold]{0}[/]",
            true when Routes.IsSupported(route) => ":rocket: [bold blue]{0}[/]",
            _ => ":construction: {0}"
        };

        table.AddRow(
            string.Format(pathTemplate, route.Path.EscapeMarkup()),
            route.RequiresAuthorization ? "Access token" : "⸻",
            route.Multilingual ? string.Join(", ", languages) : "⸻"
        );
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
