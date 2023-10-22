using System.Collections.Generic;
using GuildWars2.Meta;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace ApiVersionInfo;

internal sealed class RouteTable : IRenderable
{
    private readonly RouteOptions routeOptions;

    private readonly Table table = new Table().AddColumn("Route")
        .AddColumn("Authorization")
        .AddColumn("Localization")
        .MinimalBorder();

    public RouteTable(RouteOptions routeOptions)
    {
        this.routeOptions = routeOptions;
    }

    public Measurement Measure(RenderOptions options, int maxWidth) =>
        ((IRenderable)table).Measure(options, maxWidth);

    public IEnumerable<Segment> Render(RenderOptions options, int maxWidth) =>
        ((IRenderable)table).Render(options, maxWidth);

    public void AddRoute(Route route, IReadOnlyCollection<string> languages)
    {
        if (route.RequiresAuthorization && !routeOptions.ShowAuthorized)
        {
            return;
        }

        if (!route.Active && !routeOptions.ShowDisabled)
        {
            return;
        }

        var pathTemplate = route.Active switch
        {
            false => ":no_entry: [dim bold]{0}[/]",
            true when Routes.IsSupported(route) => ":rocket: [bold blue]{0}[/]",
            true when Routes.IsProblematic(route) => ":bug: [dim]{0}[/]",
            _ => ":construction: {0}"
        };

        table.AddRow(
            string.Format(pathTemplate, route.Path.EscapeMarkup()),
            route.RequiresAuthorization ? "Access token" : "⸻",
            route.Multilingual ? string.Join(", ", languages) : "⸻"
        );
    }
}
