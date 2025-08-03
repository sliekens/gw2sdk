using ApiVersionInfo;

using GuildWars2;
using GuildWars2.Metadata;

using Spectre.Console;

// First configure the HttpClient
// In a real application, you would use Polly and IHttpClientFactory to add resiliency etc.
// This is just the minimal setup to get things going.
using var http = new HttpClient();

// Now you can create a Gw2Client with your HttpClient object
var gw2 = new Gw2Client(http);
Build value = await gw2.Metadata.GetBuild().ValueOnly();

AnsiConsole.MarkupLine($"Gw2: [white on dodgerblue2]{value.Id}[/]");
RouteOptions options = RouteOptions.Prompt();
var routes = new RouteTable(options);

ApiVersion v1 = await gw2.Metadata.GetApiVersion("v1").ValueOnly();
foreach (Route route in v1.Routes)
{
    routes.AddRoute(route, v1.Languages);
}

ApiVersion v2 = await gw2.Metadata.GetApiVersion().ValueOnly();
foreach (Route route in v2.Routes)
{
    routes.AddRoute(route, v2.Languages);
}

var changes = new ReleaseNoteTable();
foreach (Schema schemaVersion in v2.SchemaVersions)
{
    changes.AddRow(schemaVersion);
}

AnsiConsole.WriteLine("Highlighted routes are supported by GW2SDK. Dimmed routes are unavailable.");

AnsiConsole.Write(routes);
AnsiConsole.Write(new Rule("Notable changes").LeftJustified());
AnsiConsole.Write(changes);
