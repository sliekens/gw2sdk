using System.Text.Json;
using GuildWars2.Collections;
using GuildWars2.Http;
using GuildWars2.Json;
using GuildWars2.Worlds;
using GuildWars2.Wvw.Abilities;
using GuildWars2.Wvw.Guilds;
using GuildWars2.Wvw.Matches;
using GuildWars2.Wvw.Matches.Overview;
using GuildWars2.Wvw.Matches.Scores;
using GuildWars2.Wvw.Matches.Stats;
using GuildWars2.Wvw.Objectives;
using GuildWars2.Wvw.Ranks;
using GuildWars2.Wvw.Timers;
using GuildWars2.Wvw.Upgrades;

namespace GuildWars2.Wvw;

/// <summary>Provides query methods for WvW matches, objectives, abilities, ranks, and upgrades.</summary>
[PublicAPI]
public sealed class WvwClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="WvwClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public WvwClient(HttpClient httpClient)
    {
        ThrowHelper.ThrowIfNull(httpClient);
        this.httpClient = httpClient;
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/wvw

    /// <summary>Retrieves the WvW guild associated with the account. This endpoint is only accessible with a valid access
    /// token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(AccountWvwGuild Value, MessageContext Context)> GetAccountWvwGuild(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/account/wvw", accessToken);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            var value = response.Json.RootElement.GetAccountWvwGuild();
            return (value, response.Context);
        }
    }

    #endregion v2/account/wvw

    #region v2/wvw/guilds

    /// <summary>Retrieves WvW guilds by region.</summary>
    /// <param name="region">The world region (NA or EU).</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<WvwGuild> Value, MessageContext Context)> GetWvwGuilds(
        WorldRegion region,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var id = region switch
        {
            WorldRegion.NorthAmerica => "na",
            WorldRegion.Europe => "eu",
            _ => region.ToString()
        };
        var requestBuilder = RequestBuilder.HttpGet($"v2/wvw/guilds/{id}");
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ValueHashSet<WvwGuild> wvwGuilds = [.. response.Json.RootElement.GetMap(static (in JsonElement value) => value.GetStringRequired())
                .Select(map => new WvwGuild
                {
                    Name = map.Key,
                    TeamId = map.Value
                })];

            return (wvwGuilds, response.Context);
        }
    }

    #endregion v2/wvw/guilds

    #region v2/wvw/abilities

    /// <summary>Retrieves all World Abilities.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Ability> Value, MessageContext Context)> GetAbilities(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/abilities");
        requestBuilder.Query.AddAllIds();
        requestBuilder.Query.AddLanguage(language);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetAbility());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the IDs of all World Abilities.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<int> Value, MessageContext Context)> GetAbilitiesIndex(
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/abilities");
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetInt32());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a World Ability by its ID.</summary>
    /// <param name="abilityId">The World Ability ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(Ability Value, MessageContext Context)> GetAbilityById(
        int abilityId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/abilities");
        requestBuilder.Query.AddId(abilityId);
        requestBuilder.Query.AddLanguage(language);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetAbility();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves World Abilities by their IDs.</summary>
    /// <param name="abilityIds">The World Ability IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Ability> Value, MessageContext Context)> GetAbilitiesByIds(
        IEnumerable<int> abilityIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/abilities");
        requestBuilder.Query.AddIds(abilityIds);
        requestBuilder.Query.AddLanguage(language);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetAbility());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of World Abilities.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Ability> Value, MessageContext Context)> GetAbilitiesByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/abilities");
        requestBuilder.Query.AddPage(pageIndex, pageSize);
        requestBuilder.Query.AddLanguage(language);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetAbility());
            return (value, response.Context);
        }
    }

    #endregion v2/wvw/abilities

    #region v2/wvw/objectives

    /// <summary>Retrieves all WvW objectives.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Objective> Value, MessageContext Context)> GetObjectives(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/objectives");
        requestBuilder.Query.AddAllIds();
        requestBuilder.Query.AddLanguage(language);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetObjective());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the IDs of all WvW objectives.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<string> Value, MessageContext Context)> GetObjectivesIndex(
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/objectives");
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetStringRequired());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a WvW objective by its ID.</summary>
    /// <param name="objectiveId">The WvW objective ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(Objective Value, MessageContext Context)> GetObjectiveById(
        string objectiveId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/objectives");
        requestBuilder.Query.AddId(objectiveId);
        requestBuilder.Query.AddLanguage(language);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetObjective();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves WvW objectives by their IDs.</summary>
    /// <param name="objectiveIds">The WvW objective IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Objective> Value, MessageContext Context)> GetObjectivesByIds(
        IEnumerable<string> objectiveIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/objectives");
        requestBuilder.Query.AddIds(objectiveIds);
        requestBuilder.Query.AddLanguage(language);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetObjective());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of WvW objectives.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Objective> Value, MessageContext Context)> GetObjectivesByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/objectives");
        requestBuilder.Query.AddPage(pageIndex, pageSize);
        requestBuilder.Query.AddLanguage(language);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetObjective());
            return (value, response.Context);
        }
    }

    #endregion v2/wvw/objectives

    #region v2/wvw/ranks

    /// <summary>Retrieves all World Ranks.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Rank> Value, MessageContext Context)> GetRanks(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/ranks");
        requestBuilder.Query.AddAllIds();
        requestBuilder.Query.AddLanguage(language);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetRank());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the IDs of all World Ranks.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<int> Value, MessageContext Context)> GetRanksIndex(
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/ranks");
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetInt32());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a World Rank by its ID.</summary>
    /// <param name="rankId">The World Rank ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(Rank Value, MessageContext Context)> GetRankById(
        int rankId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/ranks");
        requestBuilder.Query.AddId(rankId);
        requestBuilder.Query.AddLanguage(language);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetRank();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves World Ranks by their IDs.</summary>
    /// <param name="rankIds">The World Rank IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Rank> Value, MessageContext Context)> GetRanksByIds(
        IEnumerable<int> rankIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/ranks");
        requestBuilder.Query.AddIds(rankIds);
        requestBuilder.Query.AddLanguage(language);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetRank());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of World Ranks.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Rank> Value, MessageContext Context)> GetRanksByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/ranks");
        requestBuilder.Query.AddPage(pageIndex, pageSize);
        requestBuilder.Query.AddLanguage(language);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetRank());
            return (value, response.Context);
        }
    }

    #endregion v2/wvw/ranks

    #region v2/wvw/upgrades

    /// <summary>Retrieves all WvW objective upgrades.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<ObjectiveUpgrade> Value, MessageContext Context)> GetUpgrades(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/upgrades");
        requestBuilder.Query.AddAllIds();
        requestBuilder.Query.AddLanguage(language);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value =
                response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetObjectiveUpgrade());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the IDs of all WvW objective upgrades.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<int> Value, MessageContext Context)> GetUpgradesIndex(
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/upgrades");
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetInt32());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a WvW objective upgrade by its ID.</summary>
    /// <param name="upgradeId">The upgrade ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(ObjectiveUpgrade Value, MessageContext Context)> GetUpgradeById(
        int upgradeId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/upgrades");
        requestBuilder.Query.AddId(upgradeId);
        requestBuilder.Query.AddLanguage(language);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetObjectiveUpgrade();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves WvW objective upgrades by their IDs.</summary>
    /// <param name="upgradeIds">The upgrade IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<ObjectiveUpgrade> Value, MessageContext Context)> GetUpgradesByIds(
        IEnumerable<int> upgradeIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/upgrades");
        requestBuilder.Query.AddIds(upgradeIds);
        requestBuilder.Query.AddLanguage(language);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value =
                response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetObjectiveUpgrade());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of WvW objective upgrades.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<ObjectiveUpgrade> Value, MessageContext Context)> GetUpgradesByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/upgrades");
        requestBuilder.Query.AddPage(pageIndex, pageSize);
        requestBuilder.Query.AddLanguage(language);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value =
                response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetObjectiveUpgrade());
            return (value, response.Context);
        }
    }

    #endregion v2/wvw/upgrades

    #region v2/wvw/matches

    /// <summary>Retrieves all WvW matches.</summary>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Match> Value, MessageContext Context)> GetMatches(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/matches");
        requestBuilder.Query.AddAllIds();
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetMatch());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the IDs of all WvW matches.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<string> Value, MessageContext Context)> GetMatchesIndex(
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/matches");
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetStringRequired());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a WvW match by its ID.</summary>
    /// <param name="matchId">The match ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(Match Value, MessageContext Context)> GetMatchById(
        string matchId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/matches");
        requestBuilder.Query.AddId(matchId);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetMatch();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves WvW matches by their IDs.</summary>
    /// <param name="matchIds">The match IDs.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Match> Value, MessageContext Context)> GetMatchesByIds(
        IEnumerable<string> matchIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/matches");
        requestBuilder.Query.AddIds(matchIds);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetMatch());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of WvW matches.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Match> Value, MessageContext Context)> GetMatchesByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/matches");
        requestBuilder.Query.AddPage(pageIndex, pageSize);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetMatch());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a WvW match by one of the participating world IDs.</summary>
    /// <param name="worldId">The world ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(Match Value, MessageContext Context)> GetMatchByWorldId(
        int worldId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/matches");
        requestBuilder.Query.Add("world", worldId);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetMatch();
            return (value, response.Context);
        }
    }

    #endregion v2/wvw/matches

    #region v2/wvw/matches/overview

    /// <summary>Retrieves all WvW matches.</summary>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<MatchOverview> Value, MessageContext Context)> GetMatchesOverview(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/matches/overview");
        requestBuilder.Query.AddAllIds();
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetMatchOverview());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the IDs of all WvW matches.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<string> Value, MessageContext Context)> GetMatchesOverviewIndex(
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/matches/overview");
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetStringRequired());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a WvW match by its ID.</summary>
    /// <param name="matchId">The match ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(MatchOverview Value, MessageContext Context)> GetMatchOverviewById(
        string matchId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/matches/overview");
        requestBuilder.Query.AddId(matchId);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetMatchOverview();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves WvW matches by their IDs.</summary>
    /// <param name="matchIds">The match IDs.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<MatchOverview> Value, MessageContext Context)>
        GetMatchesOverviewByIds(
            IEnumerable<string> matchIds,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/matches/overview");
        requestBuilder.Query.AddIds(matchIds);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetMatchOverview());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of WvW matches.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<MatchOverview> Value, MessageContext Context)>
        GetMatchesOverviewByPage(
            int pageIndex,
            int? pageSize = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/matches/overview");
        requestBuilder.Query.AddPage(pageIndex, pageSize);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetMatchOverview());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a WvW match by one of the participating world IDs.</summary>
    /// <param name="worldId">The world ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(MatchOverview Value, MessageContext Context)> GetMatchOverviewByWorldId(
        int worldId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/matches/overview");
        requestBuilder.Query.Add("world", worldId);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetMatchOverview();
            return (value, response.Context);
        }
    }

    #endregion v2/wvw/matches/overview

    #region v2/wvw/matches/scores

    /// <summary>Retrieves all WvW matches.</summary>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<MatchScores> Value, MessageContext Context)> GetMatchesScores(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/matches/scores");
        requestBuilder.Query.AddAllIds();
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetMatchScores());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the IDs of all WvW matches.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<string> Value, MessageContext Context)> GetMatchesScoresIndex(
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/matches/scores");
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetStringRequired());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a WvW match by its ID.</summary>
    /// <param name="matchId">The match ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(MatchScores Value, MessageContext Context)> GetMatchScoresById(
        string matchId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/matches/scores");
        requestBuilder.Query.AddId(matchId);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetMatchScores();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves WvW matches by their IDs.</summary>
    /// <param name="matchIds">The match IDs.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<MatchScores> Value, MessageContext Context)> GetMatchesScoresByIds(
        IEnumerable<string> matchIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/matches/scores");
        requestBuilder.Query.AddIds(matchIds);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetMatchScores());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of WvW matches.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<MatchScores> Value, MessageContext Context)> GetMatchesScoresByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/matches/scores");
        requestBuilder.Query.AddPage(pageIndex, pageSize);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetMatchScores());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a WvW match by one of the participating world IDs.</summary>
    /// <param name="worldId">The world ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(MatchScores Value, MessageContext Context)> GetMatchScoresByWorldId(
        int worldId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/matches/scores");
        requestBuilder.Query.Add("world", worldId);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetMatchScores();
            return (value, response.Context);
        }
    }

    #endregion v2/wvw/matches/scores

    #region v2/wvw/matches/stats

    /// <summary>Retrieves all WvW matches.</summary>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<MatchStats> Value, MessageContext Context)> GetMatchesStats(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/matches/stats");
        requestBuilder.Query.AddAllIds();
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetMatchStats());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the IDs of all WvW matches.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<string> Value, MessageContext Context)> GetMatchesStatsIndex(
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/matches/stats");
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetStringRequired());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a WvW match by its ID.</summary>
    /// <param name="matchId">The match ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(MatchStats Value, MessageContext Context)> GetMatchStatsById(
        string matchId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/matches/stats");
        requestBuilder.Query.AddId(matchId);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetMatchStats();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves WvW matches by their IDs.</summary>
    /// <param name="matchIds">The match IDs.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<MatchStats> Value, MessageContext Context)> GetMatchesStatsByIds(
        IEnumerable<string> matchIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/matches/stats");
        requestBuilder.Query.AddIds(matchIds);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetMatchStats());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of WvW matches.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<MatchStats> Value, MessageContext Context)> GetMatchesStatsByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/matches/stats");
        requestBuilder.Query.AddPage(pageIndex, pageSize);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static (in JsonElement entry) => entry.GetMatchStats());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a WvW match by one of the participating world IDs.</summary>
    /// <param name="worldId">The world ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(MatchStats Value, MessageContext Context)> GetMatchStatsByWorldId(
        int worldId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/matches/stats");
        requestBuilder.Query.Add("world", worldId);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetMatchStats();
            return (value, response.Context);
        }
    }

    #endregion v2/wvw/matches/stats

    #region v2/wvw/timers

    /// <summary>Retrieves the timer for the current WvW guild selection lockout.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(WvwTimer Value, MessageContext Context)> GetLockoutTimer(
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/timers/lockout");
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            var value = response.Json.RootElement.GetWvwTimer();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the timer for the current WvW team assignment.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(WvwTimer Value, MessageContext Context)> GetTeamAssignmentTimer(
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/wvw/timers/teamAssignment");
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            var value = response.Json.RootElement.GetWvwTimer();
            return (value, response.Context);
        }
    }

    #endregion v2/wvw/timers
}
