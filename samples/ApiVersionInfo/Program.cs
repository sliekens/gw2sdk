using System.Net.Http;
using System.Threading.Tasks;
using GuildWars2;
using Spectre.Console;

namespace ApiVersionInfo;

public class Program
{
    public static async Task Main()
    {
        // First configure the HttpClient
        // In a real application, you would use Polly and IHttpClientFactory to add resiliency etc.
        // This is just the minimal setup to get things going.
        using var http = new HttpClient();

        // Now you can create a Gw2Client with your HttpClient object
        var gw2 = new Gw2Client(http);
        var build = await gw2.Meta.GetBuild();

        AnsiConsole.MarkupLine($"Gw2: [white on dodgerblue2]{build.Value.Id}[/]");
        var options = Options.Prompt();
        var routes = new RouteTable(options);

        var v1 = await gw2.Meta.GetApiVersion("v1");
        foreach (var route in v1.Value.Routes)
        {
            routes.AddRoute(route, v1.Value.Languages);
        }

        var v2 = await gw2.Meta.GetApiVersion();
        foreach (var route in v2.Value.Routes)
        {
            routes.AddRoute(route, v2.Value.Languages);
        }

        var changes = new ReleaseNoteTable();
        foreach (var schemaVersion in v2.Value.SchemaVersions)
        {
            changes.AddRow(schemaVersion);
        }

        AnsiConsole.WriteLine(
            "Highlighted routes are supported by GW2SDK. Dim routes are disabled."
        );

        AnsiConsole.Write(routes);
        AnsiConsole.Write(new Rule("Notable changes").LeftAligned());
        AnsiConsole.Write(changes);
    }
}

internal sealed class Options
{
    public bool ShowDisabled { get; init; }
    public bool ShowAuthorized { get; init; }

    public static Options Prompt() => new Options
    {
        ShowDisabled = AnsiConsole.Confirm("Show disabled routes?", false),
        ShowAuthorized = AnsiConsole.Confirm("Show routes that require an account?")
    };
}
