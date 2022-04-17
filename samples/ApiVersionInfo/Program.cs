using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK;
using GW2SDK.Http;
using GW2SDK.Meta;
using Spectre.Console;

namespace ApiVersionInfo;

internal class Program
{
    static Program()
    {
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en");
        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en");
    }

    private static async Task Main(string[] args)
    {
        // First configure the HttpClient
        // There are many ways to do this, but this sample takes a minimalistic approach.
        using var http = new HttpClient(new SocketsHttpHandler(), true);

        // Convenience method, sets the BaseAddress
        http.UseGuildWars2();

        // Convenience method, sets the Accept-Language header
        http.UseLanguage(Language.English);

        // End of HttpClient config
        // This is just the minimal setup to get things going without exceptions under normal conditions.
        // In a real application, you would use Polly and IHttpClientFactory to add resiliency etc.

        // From here on out, you can create GW2SDK services, pass the HttpClient and a JSON reader object.
        // The default JSON reader should work fine, but can be replaced with a custom implementation.
        var meta = new MetaQuery(http);

        var build = await AnsiConsole.Status()
            .StartAsync("Retrieving the current game version...", async ctx => await meta.GetBuild());

        AnsiConsole.MarkupLine($"Gw2: [white on dodgerblue2]{build.Value.Id}[/]");

        var metadata = await AnsiConsole.Status()
            .StartAsync("Retrieving API endpoints...",
                async ctx =>
                {
                    var v1 = await meta.GetApiVersion("v1");
                    var v2 = await meta.GetApiVersion();
                    return (v1: v1.Value, v2: v2.Value);
                });

        var showDisabled = AnsiConsole.Confirm("Show disabled routes?", false);

        var showAuthorized = AnsiConsole.Confirm("Show routes that require an account?");

        var routes = new Table().MinimalBorder()
            .AddColumn("Route")
            .AddColumn("Authorization")
            .AddColumn("Localization");

        foreach (var route in metadata.v1.Routes)
        {
            if (route.Active)
            {
                routes.AddRow(route.Path.EscapeMarkup(),
                    "⸻",
                    route.Multilingual ? string.Join(", ", metadata.v2.Languages) : "⸻");
            }
            else if (showDisabled)
            {
                routes.AddRow($"[dim]{route.Path.EscapeMarkup()}[/]",
                    route.RequiresAuthorization ? "Token" : "⸻",
                    route.Multilingual ? string.Join(", ", metadata.v2.Languages) : "⸻");
            }
        }

        foreach (var route in metadata.v2.Routes)
        {
            if (!showAuthorized && route.RequiresAuthorization)
            {
                continue;
            }

            if (route.Active)
            {
                routes.AddRow(route.Path.EscapeMarkup(),
                    route.RequiresAuthorization ? "Token" : "⸻",
                    route.Multilingual ? string.Join(", ", metadata.v2.Languages) : "⸻");
            }
            else if (showDisabled)
            {
                routes.AddRow($"[dim]{route.Path.EscapeMarkup()}[/]",
                    route.RequiresAuthorization ? "Token" : "⸻",
                    route.Multilingual ? string.Join(", ", metadata.v2.Languages) : "⸻");
            }
        }

        var changes = new Table().MinimalBorder()
            .AddColumn("Change")
            .AddColumn("Description");
        foreach (var schema in metadata.v2.SchemaVersions)
        {
            var formatted = DateTimeOffset.Parse(schema.Version)
                .ToString("D");
            changes.AddRow(formatted.EscapeMarkup(), schema.Description.EscapeMarkup());
        }

        AnsiConsole.WriteLine("The following paths are exposed by this API:");
        AnsiConsole.Write(routes);
        AnsiConsole.Write(new Rule("Notable changes").LeftAligned());
        AnsiConsole.Write(changes);
    }
}
