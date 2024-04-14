using GuildWars2.Json;
using GuildWars2.Wvw.Abilities;
using GuildWars2.Wvw.Http;
using GuildWars2.Wvw.Matches;
using GuildWars2.Wvw.Matches.Overview;
using GuildWars2.Wvw.Matches.Scores;
using GuildWars2.Wvw.Matches.Stats;
using GuildWars2.Wvw.Ranks;
using GuildWars2.Wvw.Upgrades;
using Objective = GuildWars2.Wvw.Objectives.Objective;

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
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/wvw/abilities

    /// <summary>Retrieves all World Abilities.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Ability> Value, MessageContext Context)> GetAbilities(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        AbilitiesRequest request = new()
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all World Abilities.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<int> Value, MessageContext Context)> GetAbilitiesIndex(
        CancellationToken cancellationToken = default
    )
    {
        AbilitiesIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a World Ability by its ID.</summary>
    /// <param name="abilityId">The World Ability ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(Ability Value, MessageContext Context)> GetAbilityById(
        int abilityId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        AbilityByIdRequest request = new(abilityId)
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves World Abilities by their IDs.</summary>
    /// <param name="abilityIds">The World Ability IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Ability> Value, MessageContext Context)> GetAbilitiesByIds(
        IEnumerable<int> abilityIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        AbilitiesByIdsRequest request = new(abilityIds.ToList())
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of World Abilities.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Ability> Value, MessageContext Context)> GetAbilitiesByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        AbilitiesByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/wvw/objectives

    /// <summary>Retrieves all WvW objectives.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Objective> Value, MessageContext Context)> GetObjectives(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        ObjectivesRequest request = new()
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all WvW objectives.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<string> Value, MessageContext Context)> GetObjectivesIndex(
        CancellationToken cancellationToken = default
    )
    {
        ObjectivesIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a WvW objective by its ID.</summary>
    /// <param name="objectiveId">The WvW objective ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(Objective Value, MessageContext Context)> GetObjectiveById(
        string objectiveId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        ObjectiveByIdRequest request = new(objectiveId)
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves WvW objectives by their IDs.</summary>
    /// <param name="objectiveIds">The WvW objective IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Objective> Value, MessageContext Context)> GetObjectivesByIds(
        IEnumerable<string> objectiveIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        ObjectivesByIdsRequest request = new(objectiveIds.ToList())
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of WvW objectives.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Objective> Value, MessageContext Context)> GetObjectivesByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        ObjectivesByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/wvw/ranks

    /// <summary>Retrieves all World Ranks.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Rank> Value, MessageContext Context)> GetRanks(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        RanksRequest request = new()
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all World Ranks.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<int> Value, MessageContext Context)> GetRanksIndex(
        CancellationToken cancellationToken = default
    )
    {
        RanksIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a World Rank by its ID.</summary>
    /// <param name="rankId">The World Rank ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(Rank Value, MessageContext Context)> GetRankById(
        int rankId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        RankByIdRequest request = new(rankId)
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves World Ranks by their IDs.</summary>
    /// <param name="rankIds">The World Rank IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Rank> Value, MessageContext Context)> GetRanksByIds(
        IEnumerable<int> rankIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        RanksByIdsRequest request = new(rankIds.ToList())
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of World Ranks.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Rank> Value, MessageContext Context)> GetRanksByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        RanksByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/wvw/upgrades

    /// <summary>Retrieves all WvW objective upgrades.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<ObjectiveUpgrade> Value, MessageContext Context)> GetUpgrades(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        UpgradesRequest request = new()
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all WvW objective upgrades.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<int> Value, MessageContext Context)> GetUpgradesIndex(
        CancellationToken cancellationToken = default
    )
    {
        UpgradesIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a WvW objective upgrade by its ID.</summary>
    /// <param name="upgradeId">The upgrade ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(ObjectiveUpgrade Value, MessageContext Context)> GetUpgradeById(
        int upgradeId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        UpgradeByIdRequest request = new(upgradeId)
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves WvW objective upgrades by their IDs.</summary>
    /// <param name="upgradeIds">The upgrade IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<ObjectiveUpgrade> Value, MessageContext Context)> GetUpgradesByIds(
        IEnumerable<int> upgradeIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        UpgradesByIdsRequest request = new(upgradeIds.ToList())
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of WvW objective upgrades.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<ObjectiveUpgrade> Value, MessageContext Context)> GetUpgradesByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        UpgradesByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/wvw/matches

    /// <summary>Retrieves all WvW matches.</summary>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Match> Value, MessageContext Context)> GetMatches(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MatchesRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all WvW matches.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<string> Value, MessageContext Context)> GetMatchesIndex(
        CancellationToken cancellationToken = default
    )
    {
        MatchesIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a WvW match by its ID.</summary>
    /// <param name="matchId">The match ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(Match Value, MessageContext Context)> GetMatchById(
        string matchId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MatchByIdRequest request = new(matchId);
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves WvW matches by their IDs.</summary>
    /// <param name="matchIds">The match IDs.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Match> Value, MessageContext Context)> GetMatchesByIds(
        IEnumerable<string> matchIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MatchesByIdsRequest request = new(matchIds.ToList());
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of WvW matches.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Match> Value, MessageContext Context)> GetMatchesByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MatchesByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a WvW match by one of the participating world IDs.</summary>
    /// <param name="worldId">The world ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(Match Value, MessageContext Context)> GetMatchByWorldId(
        int worldId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MatchByWorldIdRequest request = new(worldId);
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/wvw/matches/overview

    /// <summary>Retrieves all WvW matches.</summary>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<MatchOverview> Value, MessageContext Context)> GetMatchesOverview(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MatchesOverviewRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all WvW matches.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<string> Value, MessageContext Context)> GetMatchesOverviewIndex(
        CancellationToken cancellationToken = default
    )
    {
        MatchesOverviewIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a WvW match by its ID.</summary>
    /// <param name="matchId">The match ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(MatchOverview Value, MessageContext Context)> GetMatchOverviewById(
        string matchId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MatchOverviewByIdRequest request = new(matchId);
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves WvW matches by their IDs.</summary>
    /// <param name="matchIds">The match IDs.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<MatchOverview> Value, MessageContext Context)> GetMatchesOverviewByIds(
        IEnumerable<string> matchIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MatchesOverviewByIdsRequest request = new(matchIds.ToList());
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of WvW matches.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<MatchOverview> Value, MessageContext Context)> GetMatchesOverviewByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MatchesOverviewByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a WvW match by one of the participating world IDs.</summary>
    /// <param name="worldId">The world ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(MatchOverview Value, MessageContext Context)> GetMatchOverviewByWorldId(
        int worldId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MatchOverviewByWorldIdRequest request = new(worldId);
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/wvw/matches/scores

    /// <summary>Retrieves all WvW matches.</summary>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<MatchScores> Value, MessageContext Context)> GetMatchesScores(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MatchesScoresRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all WvW matches.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<string> Value, MessageContext Context)> GetMatchesScoresIndex(
        CancellationToken cancellationToken = default
    )
    {
        MatchesScoresIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a WvW match by its ID.</summary>
    /// <param name="matchId">The match ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(MatchScores Value, MessageContext Context)> GetMatchScoresById(
        string matchId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MatchScoresByIdRequest request = new(matchId);
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves WvW matches by their IDs.</summary>
    /// <param name="matchIds">The match IDs.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<MatchScores> Value, MessageContext Context)> GetMatchesScoresByIds(
        IEnumerable<string> matchIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MatchesScoresByIdsRequest request = new(matchIds.ToList());
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of WvW matches.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<MatchScores> Value, MessageContext Context)> GetMatchesScoresByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MatchesScoresByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a WvW match by one of the participating world IDs.</summary>
    /// <param name="worldId">The world ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(MatchScores Value, MessageContext Context)> GetMatchScoresByWorldId(
        int worldId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MatchScoresByWorldIdRequest request = new(worldId);
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/wvw/matches/stats

    /// <summary>Retrieves all WvW matches.</summary>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<MatchStats> Value, MessageContext Context)> GetMatchesStats(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MatchesStatsRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all WvW matches.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<string> Value, MessageContext Context)> GetMatchesStatsIndex(
        CancellationToken cancellationToken = default
    )
    {
        MatchesStatsIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a WvW match by its ID.</summary>
    /// <param name="matchId">The match ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(MatchStats Value, MessageContext Context)> GetMatchStatsById(
        string matchId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MatchStatsByIdRequest request = new(matchId);
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves WvW matches by their IDs.</summary>
    /// <param name="matchIds">The match IDs.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<MatchStats> Value, MessageContext Context)> GetMatchesStatsByIds(
        IEnumerable<string> matchIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MatchesStatsByIdsRequest request = new(matchIds.ToList());
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of WvW matches.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<MatchStats> Value, MessageContext Context)> GetMatchesStatsByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MatchesStatsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a WvW match by one of the participating world IDs.</summary>
    /// <param name="worldId">The world ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(MatchStats Value, MessageContext Context)> GetMatchStatsByWorldId(
        int worldId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MatchStatsByWorldIdRequest request = new(worldId);
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion
}
