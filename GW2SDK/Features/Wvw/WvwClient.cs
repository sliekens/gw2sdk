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

[PublicAPI]
public sealed class WvwClient
{
    private readonly HttpClient httpClient;

    public WvwClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/wvw/abilities

    public Task<(HashSet<Ability> Value, MessageContext Context)> GetAbilities(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AbilitiesRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetAbilitiesIndex(
        CancellationToken cancellationToken = default
    )
    {
        AbilitiesIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(Ability Value, MessageContext Context)> GetAbilityById(
        int abilityId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AbilityByIdRequest request = new(abilityId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Ability> Value, MessageContext Context)> GetAbilitiesByIds(
        IReadOnlyCollection<int> abilityIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AbilitiesByIdsRequest request = new(abilityIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Ability> Value, MessageContext Context)> GetAbilitiesByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AbilitiesByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/wvw/objectives

    public Task<(HashSet<Objective> Value, MessageContext Context)> GetObjectives(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ObjectivesRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<string> Value, MessageContext Context)> GetObjectivesIndex(
        CancellationToken cancellationToken = default
    )
    {
        ObjectivesIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(Objective Value, MessageContext Context)> GetObjectiveById(
        string objectiveId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ObjectiveByIdRequest request = new(objectiveId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Objective> Value, MessageContext Context)> GetObjectivesByIds(
        IReadOnlyCollection<string> objectiveIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ObjectivesByIdsRequest request = new(objectiveIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Objective> Value, MessageContext Context)> GetObjectivesByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ObjectivesByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/wvw/ranks

    public Task<(HashSet<Rank> Value, MessageContext Context)> GetRanks(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RanksRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetRanksIndex(CancellationToken cancellationToken = default)
    {
        RanksIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(Rank Value, MessageContext Context)> GetRankById(
        int rankId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RankByIdRequest request = new(rankId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Rank> Value, MessageContext Context)> GetRanksByIds(
        IReadOnlyCollection<int> rankIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RanksByIdsRequest request = new(rankIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Rank> Value, MessageContext Context)> GetRanksByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RanksByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/wvw/upgrades

    public Task<(HashSet<ObjectiveUpgrade> Value, MessageContext Context)> GetUpgrades(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        UpgradesRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetUpgradesIndex(
        CancellationToken cancellationToken = default
    )
    {
        UpgradesIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(ObjectiveUpgrade Value, MessageContext Context)> GetUpgradeById(
        int upgradeId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        UpgradeByIdRequest request = new(upgradeId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<ObjectiveUpgrade> Value, MessageContext Context)> GetUpgradesByIds(
        IReadOnlyCollection<int> upgradeIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        UpgradesByIdsRequest request = new(upgradeIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<ObjectiveUpgrade> Value, MessageContext Context)> GetUpgradesByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        UpgradesByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/wvw/matches

    public Task<(HashSet<Match> Value, MessageContext Context)> GetMatches(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MatchesRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<string> Value, MessageContext Context)> GetMatchesIndex(
        CancellationToken cancellationToken = default
    )
    {
        MatchesIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(Match Value, MessageContext Context)> GetMatchById(
        string matchId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MatchByIdRequest request = new(matchId) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Match> Value, MessageContext Context)> GetMatchesByIds(
        IReadOnlyCollection<string> matchIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MatchesByIdsRequest request = new(matchIds)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Match> Value, MessageContext Context)> GetMatchesByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MatchesByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(Match Value, MessageContext Context)> GetMatchByWorldId(
        int worldId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MatchByWorldIdRequest request = new(worldId)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/wvw/matches/overview

    public Task<(HashSet<MatchOverview> Value, MessageContext Context)> GetMatchesOverview(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MatchesOverviewRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<string> Value, MessageContext Context)> GetMatchesOverviewIndex(
        CancellationToken cancellationToken = default
    )
    {
        MatchesOverviewIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(MatchOverview Value, MessageContext Context)> GetMatchOverviewById(
        string matchId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MatchOverviewByIdRequest request = new(matchId)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<MatchOverview> Value, MessageContext Context)> GetMatchesOverviewByIds(
        IReadOnlyCollection<string> matchIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MatchesOverviewByIdsRequest request = new(matchIds)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<MatchOverview> Value, MessageContext Context)> GetMatchesOverviewByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MatchesOverviewByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(MatchOverview Value, MessageContext Context)> GetMatchOverviewByWorldId(
        int worldId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MatchOverviewByWorldIdRequest request = new(worldId)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/wvw/matches/scores

    public Task<(HashSet<MatchScores> Value, MessageContext Context)> GetMatchesScores(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MatchesScoresRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<string> Value, MessageContext Context)> GetMatchesScoresIndex(
        CancellationToken cancellationToken = default
    )
    {
        MatchesScoresIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(MatchScores Value, MessageContext Context)> GetMatchScoresById(
        string matchId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MatchScoresByIdRequest request = new(matchId)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<MatchScores> Value, MessageContext Context)> GetMatchesScoresByIds(
        IReadOnlyCollection<string> matchIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MatchesScoresByIdsRequest request = new(matchIds)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<MatchScores> Value, MessageContext Context)> GetMatchesScoresByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MatchesScoresByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(MatchScores Value, MessageContext Context)> GetMatchScoresByWorldId(
        int worldId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MatchScoresByWorldIdRequest request = new(worldId)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/wvw/matches/stats

    public Task<(HashSet<MatchStats> Value, MessageContext Context)> GetMatchesStats(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MatchesStatsRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<string> Value, MessageContext Context)> GetMatchesStatsIndex(
        CancellationToken cancellationToken = default
    )
    {
        MatchesStatsIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(MatchStats Value, MessageContext Context)> GetMatchStatsById(
        string matchId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MatchStatsByIdRequest request = new(matchId)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<MatchStats> Value, MessageContext Context)> GetMatchesStatsByIds(
        IReadOnlyCollection<string> matchIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MatchesStatsByIdsRequest request = new(matchIds)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<MatchStats> Value, MessageContext Context)> GetMatchesStatsByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MatchesStatsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(MatchStats Value, MessageContext Context)> GetMatchStatsByWorldId(
        int worldId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MatchStatsByWorldIdRequest request = new(worldId)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion
}
