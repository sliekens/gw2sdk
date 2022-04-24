using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GW2SDK;
using GW2SDK.Http;
using Spectre.Console;

namespace ApiVersionInfo;

internal class Program
{
    /// <summary>Set of endpoints supported by GW2SDK.</summary>
    private static readonly IReadOnlySet<string> Supported = new HashSet<string>
    {
        "/v2/account",
        "/v2/account/achievements",
        "/v2/account/bank",
        "/v2/account/buildstorage",
        "/v2/account/dailycrafting",
        "/v2/account/dungeons",
        "/v2/account/dyes",
        //"/v2/account/emotes",
        //"/v2/account/finishers",
        //"/v2/account/gliders",
        "/v2/account/home",
        "/v2/account/home/cats",
        "/v2/account/home/nodes",
        "/v2/account/inventory",
        //"/v2/account/legendaryarmory",
        //"/v2/account/luck",
        "/v2/account/mailcarriers",
        //"/v2/account/mapchests",
        "/v2/account/masteries",
        //"/v2/account/mastery/points",
        //"/v2/account/materials",
        //"/v2/account/minis",
        //"/v2/account/mounts",
        //"/v2/account/mounts/skins",
        //"/v2/account/mounts/types",
        //"/v2/account/novelties",
        //"/v2/account/outfits",
        //"/v2/account/progression",
        //"/v2/account/pvp/heroes",
        //"/v2/account/raids",
        "/v2/account/recipes",
        //"/v2/account/skins",
        //"/v2/account/titles",
        "/v2/account/wallet",
        "/v2/account/worldbosses",
        "/v2/achievements",
        "/v2/achievements/categories",
        "/v2/achievements/daily",
        "/v2/achievements/daily/tomorrow",
        "/v2/achievements/groups",
        "/v2/backstory/answers",
        "/v2/backstory/questions",
        "/v2/build",
        "/v2/characters",
        //"/v2/characters/:id/backstory",
        //"/v2/characters/:id/buildtabs",
        //"/v2/characters/:id/buildtabs/active",
        //"/v2/characters/:id/core",
        //"/v2/characters/:id/crafting",
        //"/v2/characters/:id/equipment",
        //"/v2/characters/:id/equipmenttabs",
        //"/v2/characters/:id/equipmenttabs/active",
        //"/v2/characters/:id/heropoints",
        //"/v2/characters/:id/inventory",
        //"/v2/characters/:id/quests",
        //"/v2/characters/:id/recipes",
        //"/v2/characters/:id/sab",
        //"/v2/characters/:id/skills",
        //"/v2/characters/:id/specializations",
        //"/v2/characters/:id/training",
        "/v2/colors",
        "/v2/commerce/delivery",
        "/v2/commerce/exchange",
        "/v2/commerce/listings",
        "/v2/commerce/prices",
        "/v2/commerce/transactions",
        "/v2/continents",
        "/v2/createsubtoken",
        "/v2/currencies",
        "/v2/dailycrafting",
        "/v2/dungeons",
        //"/v2/emblem",
        //"/v2/emotes",
        //"/v2/files",
        //"/v2/finishers",
        //"/v2/gliders",
        //"/v2/guild/:id",
        //"/v2/guild/:id/log",
        //"/v2/guild/:id/members",
        //"/v2/guild/:id/ranks",
        //"/v2/guild/:id/stash",
        //"/v2/guild/:id/storage",
        //"/v2/guild/:id/teams",
        //"/v2/guild/:id/treasury",
        //"/v2/guild/:id/upgrades",
        //"/v2/guild/permissions",
        //"/v2/guild/search",
        //"/v2/guild/upgrades",
        "/v2/home",
        "/v2/home/cats",
        "/v2/home/nodes",
        "/v2/items",
        "/v2/itemstats",
        //"/v2/legendaryarmory",
        //"/v2/legends",
        "/v2/mailcarriers",
        //"/v2/mapchests",
        //"/v2/maps",
        "/v2/masteries",
        "/v2/materials",
        //"/v2/minis",
        "/v2/mounts",
        "/v2/mounts/skins",
        "/v2/mounts/types",
        //"/v2/novelties",
        //"/v2/outfits",
        //"/v2/pets",
        "/v2/professions",
        //"/v2/pvp",
        //"/v2/pvp/amulets",
        //"/v2/pvp/games",
        //"/v2/pvp/heroes",
        //"/v2/pvp/ranks",
        //"/v2/pvp/seasons",
        //"/v2/pvp/seasons/:id/leaderboards",
        //"/v2/pvp/seasons/:id/leaderboards/:board/:region",
        //"/v2/pvp/standings",
        //"/v2/pvp/stats",
        "/v2/quaggans",
        //"/v2/quests",
        //"/v2/races",
        //"/v2/raids",
        "/v2/recipes",
        "/v2/recipes/search",
        "/v2/skills",
        "/v2/skins",
        "/v2/specializations",
        //"/v2/stories",
        //"/v2/stories/seasons",
        "/v2/titles",
        "/v2/tokeninfo",
        "/v2/traits",
        "/v2/worldbosses",
        "/v2/worlds",
        //"/v2/wvw/abilities",
        //"/v2/wvw/matches",
        //"/v2/wvw/matches/overview",
        //"/v2/wvw/matches/scores",
        //"/v2/wvw/matches/stats",
        //"/v2/wvw/matches/stats/:id/guilds/:guild_id",
        //"/v2/wvw/matches/stats/:id/teams/:team/top/kdr",
        //"/v2/wvw/matches/stats/:id/teams/:team/top/kills",
        //"/v2/wvw/objectives",
        //"/v2/wvw/ranks",
        //"/v2/wvw/upgrades"
    };

    static Program()
    {
        Console.OutputEncoding = Encoding.UTF8;
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
            .StartAsync(
                "Retrieving the current game version...",
                async ctx => await meta.GetBuild()
                );

        AnsiConsole.MarkupLine($"Gw2: [white on dodgerblue2]{build.Value.Id}[/]");

        var metadata = await AnsiConsole.Status()
            .StartAsync(
                "Retrieving API endpoints...",
                async ctx =>
                {
                    var v1 = await meta.GetApiVersion("v1");
                    var v2 = await meta.GetApiVersion();
                    return (v1: v1.Value, v2: v2.Value);
                }
                );

        var showDisabled = AnsiConsole.Confirm("Show disabled routes?", false);

        var showAuthorized = AnsiConsole.Confirm("Show routes that require an account?");

        var routes = new Table().MinimalBorder()
            .AddColumn("Route")
            .AddColumn("Authorization")
            .AddColumn("Localization");

        foreach (var route in metadata.v1.Routes)
        {
            var pathTemplate = route.Active switch
            {
                true when Supported.Contains(route.Path) => "[bold blue]{0}[/]",
                false => "[dim]{0}[/]",
                _ => "{0}"
            };

            if (route.Active || showDisabled)
            {
                routes.AddRow(
                    string.Format(pathTemplate, route.Path.EscapeMarkup()),
                    "⸻",
                    route.Multilingual ? string.Join(", ", metadata.v1.Languages) : "⸻"
                    );
            }
        }

        foreach (var route in metadata.v2.Routes)
        {
            if (!showAuthorized && route.RequiresAuthorization)
            {
                continue;
            }

            var pathTemplate = route.Active switch
            {
                true when Supported.Contains(route.Path) => "[bold blue]{0}[/]",
                false => "[dim]{0}[/]",
                _ => "{0}"
            };

            if (route.Active || showDisabled)
            {
                routes.AddRow(
                    string.Format(pathTemplate, route.Path.EscapeMarkup()),
                    "⸻",
                    route.Multilingual ? string.Join(", ", metadata.v2.Languages) : "⸻"
                    );
            }
        }

        var changes = new Table().MinimalBorder().AddColumn("Change").AddColumn("Description");
        foreach (var schema in metadata.v2.SchemaVersions)
        {
            var formatted = DateTimeOffset.Parse(schema.Version).ToString("D");
            changes.AddRow(formatted.EscapeMarkup(), schema.Description.EscapeMarkup());
        }

        AnsiConsole.WriteLine("Highlighted routes are supported by GW2SDK. Dim routes are disabled.");
        AnsiConsole.Write(routes);
        AnsiConsole.Write(new Rule("Notable changes").LeftAligned());
        AnsiConsole.Write(changes);
    }
}
