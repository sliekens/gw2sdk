using GuildWars2.Metadata;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace ApiVersionInfo;

internal sealed class RouteTable(RouteOptions routeOptions) : IRenderable
{
    private readonly Table table = new Table().AddColumn("Route")
        .AddColumn("Authorization")
        .AddColumn("Localization")
        .MinimalBorder();

    public Measurement Measure(RenderOptions options, int maxWidth)
    {
        return ((IRenderable)table).Measure(options, maxWidth);
    }

    public IEnumerable<Segment> Render(RenderOptions options, int maxWidth)
    {
        return ((IRenderable)table).Render(options, maxWidth);
    }

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
            true when Routes.IsObsolete(route) => ":sauropod: [dim]{0}[/]",
            true when Routes.IsProblematic(route) => ":bug: [dim]{0}[/]",
            true when Routes.IsSupported(route) => ":rocket: [bold blue]{0}[/]",
            _ => ":construction: {0}"
        };

        table.AddRow(
            string.Format(pathTemplate, route.Path.EscapeMarkup()),
            route.RequiresAuthorization ? "Access token" : "⸻",
            route.Multilingual ? string.Join(", ", languages) : "⸻"
        );
    }
}
