﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Metadata;

public class V2
{
    [Fact]
    public async Task Has_api_metadata()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, _) =
            await sut.Metadata.GetApiVersion(
                cancellationToken: TestContext.Current.CancellationToken
            );

        Assert.All(
            actual.Languages,
            language =>
            {
                Assert.Contains(
                    language,
                    new[]
                    {
                        "en",
                        "es",
                        "de",
                        "fr",
                        "zh"
                    }
                );
            }
        );

        // curl -s https://api.guildwars2.com/v2.json?v=latest | jq '.routes[].path' | sort | awk '{print $0 ","}'
        HashSet<string> routes =
        [
            "/v2/account", "/v2/account/achievements",
            "/v2/account/bank", "/v2/account/buildstorage",
            "/v2/account/dailycrafting", "/v2/account/dungeons",
            "/v2/account/dyes", "/v2/account/emotes",
            "/v2/account/finishers", "/v2/account/gliders",
            "/v2/account/home", "/v2/account/home/cats",
            "/v2/account/home/nodes", "/v2/account/homestead",
            "/v2/account/homestead/decorations", "/v2/account/homestead/glyphs",
            "/v2/account/inventory", "/v2/account/jadebots",
            "/v2/account/legendaryarmory", "/v2/account/luck",
            "/v2/account/mail", "/v2/account/mailcarriers",
            "/v2/account/mapchests", "/v2/account/masteries",
            "/v2/account/mastery/points", "/v2/account/materials",
            "/v2/account/minis", "/v2/account/mounts",
            "/v2/account/mounts/skins", "/v2/account/mounts/types",
            "/v2/account/novelties", "/v2/account/outfits",
            "/v2/account/progression", "/v2/account/pvp/heroes",
            "/v2/account/raids", "/v2/account/recipes",
            "/v2/account/skiffs", "/v2/account/skins",
            "/v2/account/titles", "/v2/account/wallet",
            "/v2/account/wizardsvault/daily", "/v2/account/wizardsvault/listings",
            "/v2/account/wizardsvault/special", "/v2/account/wizardsvault/weekly",
            "/v2/account/worldbosses", "/v2/account/wvw",
            "/v2/achievements", "/v2/achievements/categories",
            "/v2/achievements/daily", "/v2/achievements/daily/tomorrow",
            "/v2/achievements/groups", "/v2/adventures",
            "/v2/adventures/:id/leaderboards", "/v2/adventures/:id/leaderboards/:board/:region",
            "/v2/backstory/answers", "/v2/backstory/questions",
            "/v2/build", "/v2/characters",
            "/v2/characters/:id/backstory", "/v2/characters/:id/buildtabs",
            "/v2/characters/:id/buildtabs/active", "/v2/characters/:id/core",
            "/v2/characters/:id/crafting", "/v2/characters/:id/dungeons",
            "/v2/characters/:id/equipment", "/v2/characters/:id/equipmenttabs",
            "/v2/characters/:id/equipmenttabs/active", "/v2/characters/:id/heropoints",
            "/v2/characters/:id/inventory", "/v2/characters/:id/quests",
            "/v2/characters/:id/recipes", "/v2/characters/:id/sab",
            "/v2/characters/:id/skills", "/v2/characters/:id/specializations",
            "/v2/characters/:id/training", "/v2/colors",
            "/v2/commerce/delivery", "/v2/commerce/exchange",
            "/v2/commerce/listings", "/v2/commerce/prices",
            "/v2/commerce/transactions", "/v2/continents",
            "/v2/createsubtoken", "/v2/currencies",
            "/v2/dailycrafting", "/v2/dungeons",
            "/v2/emblem", "/v2/emotes",
            "/v2/events", "/v2/events-state",
            "/v2/files", "/v2/finishers",
            "/v2/gemstore/catalog", "/v2/gliders",
            "/v2/guild/:id", "/v2/guild/:id/log",
            "/v2/guild/:id/members", "/v2/guild/:id/ranks",
            "/v2/guild/:id/stash", "/v2/guild/:id/storage",
            "/v2/guild/:id/teams", "/v2/guild/:id/treasury",
            "/v2/guild/:id/upgrades", "/v2/guild/permissions",
            "/v2/guild/search", "/v2/guild/upgrades",
            "/v2/home", "/v2/home/cats",
            "/v2/home/nodes", "/v2/homestead",
            "/v2/homestead", "/v2/homestead/decorations",
            "/v2/homestead/decorations/categories", "/v2/homestead/decorations/categories/:id",
            "/v2/homestead/glyphs", "/v2/items",
            "/v2/itemstats", "/v2/jadebots",
            "/v2/legendaryarmory", "/v2/legends",
            "/v2/logos",
            "/v2/mailcarriers", "/v2/mapchests",
            "/v2/maps", "/v2/masteries",
            "/v2/materials", "/v2/minis",
            "/v2/mounts", "/v2/mounts/skins",
            "/v2/mounts/types", "/v2/novelties",
            "/v2/outfits", "/v2/pets",
            "/v2/professions", "/v2/pvp",
            "/v2/pvp/amulets", "/v2/pvp/games",
            "/v2/pvp/heroes", "/v2/pvp/ranks",
            "/v2/pvp/rewardtracks", "/v2/pvp/runes",
            "/v2/pvp/seasons", "/v2/pvp/seasons/:id/leaderboards",
            "/v2/pvp/seasons/:id/leaderboards/:board/:region", "/v2/pvp/sigils",
            "/v2/pvp/standings", "/v2/pvp/stats",
            "/v2/quaggans", "/v2/quests",
            "/v2/races", "/v2/raids",
            "/v2/recipes", "/v2/recipes/search",
            "/v2/skiffs", "/v2/skills",
            "/v2/skins", "/v2/specializations",
            "/v2/stories", "/v2/stories/seasons",
            "/v2/titles", "/v2/tokeninfo",
            "/v2/traits", "/v2/vendors",
            "/v2/wizardsvault/listings", "/v2/wizardsvault/objectives",
            "/v2/worldbosses", "/v2/worlds",
            "/v2/wvw/abilities", "/v2/wvw/guilds",
            "/v2/wvw/guilds/:region", "/v2/wvw/matches",
            "/v2/wvw/matches/overview", "/v2/wvw/matches/scores",
            "/v2/wvw/matches/stats", "/v2/wvw/matches/stats/:id/guilds/:guild_id",
            "/v2/wvw/matches/stats/:id/teams/:team/top/kdr",
            "/v2/wvw/matches/stats/:id/teams/:team/top/kills", "/v2/wvw/objectives",
            "/v2/wvw/ranks", "/v2/wvw/rewardtracks",
            "/v2/wvw/timers", "/v2/wvw/timers/lockout",
            "/v2/wvw/timers/teamAssignment", "/v2/wvw/upgrades"
        ];

        Assert.All(
            actual.Routes,
            route =>
            {
                Assert.Contains(route.Path, routes);
            }
        );
        Assert.Collection(
            actual.SchemaVersions,
            v => Assert.Equal(SchemaVersion.V20190221, v.Version),
            v => Assert.Equal(SchemaVersion.V20190322, v.Version),
            v => Assert.Equal(SchemaVersion.V20190516, v.Version),
            v => Assert.Equal(SchemaVersion.V20190521, v.Version),
            v => Assert.Equal(SchemaVersion.V20190522, v.Version),
            v => Assert.Equal(SchemaVersion.V20191219, v.Version),
            v => Assert.Equal(SchemaVersion.V20201117, v.Version),
            v => Assert.Equal(SchemaVersion.V20210406, v.Version),
            v => Assert.Equal(SchemaVersion.V20210715, v.Version),
            v => Assert.Equal(SchemaVersion.V20220309, v.Version),
            v => Assert.Equal(SchemaVersion.V20220323, v.Version),
            v => Assert.Equal(SchemaVersion.V20240720, v.Version)
        );
    }
}
