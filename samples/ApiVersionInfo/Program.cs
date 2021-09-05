using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK;
using GW2SDK.Builds;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.V2;
using Spectre.Console;

namespace ApiVersionInfo
{
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

            // Convenience method, sets the X-Schema-Version to the recommended version
            // (where recommended means that it is the least likely to cause JSON conversion errors)
            http.UseSchemaVersion(SchemaVersion.Recommended);

            // End of HttpClient config
            // This is just the minimal setup to get things going without exceptions under normal conditions.
            // In a real application, you would use Polly and IHttpClientFactory to add resiliency etc.

            // From here on out, you can create GW2SDK services, pass the HttpClient and a JSON reader object.
            // The default JSON reader should work fine, but can be replaced with a custom implementation.
            var buildService = new BuildService(http, new BuildReader(), MissingMemberBehavior.Undefined);
            var infoService = new ApiInfoService(http, new ApiInfoReader(), MissingMemberBehavior.Undefined);

            var build = await AnsiConsole.Status()
                .StartAsync("Retrieving the current game version...", async ctx => await buildService.GetBuild());

            AnsiConsole.MarkupLine($"Gw2: [white on dodgerblue2]{build.Value.Id}[/]");

            var metadata = await AnsiConsole.Status()
                .StartAsync("Retrieving API endpoints...",
                    async ctx => await infoService.GetApiInfo());

            var showDisabled = AnsiConsole.Confirm("Show disabled routes?", false);

            var showAuthorized = AnsiConsole.Confirm("Show routes that require an account?");

            var routes = new Table().MinimalBorder()
                .AddColumn("Route")
                .AddColumn("Authorization")
                .AddColumn("Localization");

            foreach (var route in metadata.Value.Routes)
            {
                if (!showAuthorized && route.RequiresAuthorization)
                {
                    continue;
                }

                if (route.Active)
                {
                    routes.AddRow(route.Path.EscapeMarkup(),
                        route.RequiresAuthorization ? "Requires token" : "Anonymous",
                        route.Multilingual ? string.Join(", ", metadata.Value.Languages) : "");
                }
                else if (showDisabled)
                {
                    routes.AddRow($"[dim]{route.Path.EscapeMarkup()}[/]",
                        route.RequiresAuthorization ? "Requires token" : "Anonymous",
                        route.Multilingual ? string.Join(", ", metadata.Value.Languages) : "");
                }
            }

            var changes = new Table().MinimalBorder()
                .AddColumn("Change")
                .AddColumn("Description");
            foreach (var schema in metadata.Value.SchemaVersions)
            {
                var formatted = DateTimeOffset.Parse(schema.Version)
                    .ToString("D");
                changes.AddRow(formatted.EscapeMarkup(), schema.Description.EscapeMarkup());
            }

            AnsiConsole.WriteLine("The following paths are exposed by this API:");
            AnsiConsole.Render(routes);
            AnsiConsole.Render(new Rule("Notable changes").LeftAligned());
            AnsiConsole.Render(changes);
        }
    }
}
