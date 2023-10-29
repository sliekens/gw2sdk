using GuildWars2.Pvp.Amulets;
using GuildWars2.Pvp.Games;
using GuildWars2.Pvp.Heroes;
using GuildWars2.Pvp.Http;
using GuildWars2.Pvp.Ranks;
using GuildWars2.Pvp.Seasons;
using GuildWars2.Pvp.Standings;
using GuildWars2.Pvp.Stats;

namespace GuildWars2.Pvp;

[PublicAPI]
public sealed class PvpQuery
{
    private readonly HttpClient http;

    public PvpQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/pvp/seasons/:id/leaderboards

    public Task<Replica<HashSet<string>>> GetLeaderboards(
        string seasonId,
        CancellationToken cancellationToken = default
    )
    {
        LeaderboardsRequest request = new(seasonId);
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/pvp/seasons/:id/leaderboards/:board

    public Task<Replica<HashSet<string>>> GetLeaderboardRegions(
        string seasonId,
        string boardId,
        CancellationToken cancellationToken = default
    )
    {
        LeaderboardRegionsRequest request = new(seasonId, boardId);
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/pvp/seasons/:id/leaderboards/:board/:region

    public Task<Replica<HashSet<LeaderboardEntry>>> GetLeaderboardEntries(
        string seasonId,
        string boardId,
        string regionId,
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        LeaderboardEntriesRequest request = new(seasonId, boardId, regionId, pageIndex)
        {
            PageSize = pageSize,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/pvp/standings

    public Task<Replica<HashSet<Standing>>> GetStandings(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        StandingsRequest request = new()
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/pvp/stats

    public Task<Replica<AccountStats>> GetStats(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        StatsRequest request = new()
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/pvp/amulets

    public Task<Replica<HashSet<Amulet>>> GetAmulets(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AmuletRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<int>>> GetAmuletsIndex(
        CancellationToken cancellationToken = default
    )
    {
        AmuletIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<Amulet>> GetAmuletById(
        int amuletId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AmuletByIdRequest request = new(amuletId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Amulet>>> GetAmuletsByIds(
        IReadOnlyCollection<int> amuletIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AmuletsByIdsRequest request = new(amuletIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Amulet>>> GetAmuletsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AmuletsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/pvp/heroes

    public Task<Replica<HashSet<Hero>>> GetHeroes(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        HeroRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<string>>> GetHeroesIndex(
        CancellationToken cancellationToken = default
    )
    {
        HeroIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<Hero>> GetHeroById(
        string heroId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        HeroByIdRequest request = new(heroId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Hero>>> GetHeroesByIds(
        IReadOnlyCollection<string> heroIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        HeroesByIdsRequest request = new(heroIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Hero>>> GetHeroesByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        HeroesByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/account/pvp/heroes

    public Task<Replica<HashSet<int>>> GetUnlockedHeroesIndex(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        UnlockedHeroesRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/account/pvp/heroes

    #region v2/pvp/ranks

    public Task<Replica<HashSet<Rank>>> GetRanks(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RankRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<int>>> GetRanksIndex(CancellationToken cancellationToken = default)
    {
        RankIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<Rank>> GetRankById(
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
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Rank>>> GetRanksByIds(
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
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Rank>>> GetRanksByPage(
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
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/pvp/seasons

    public Task<Replica<HashSet<Season>>> GetSeasons(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SeasonRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<string>>> GetSeasonsIndex(
        CancellationToken cancellationToken = default
    )
    {
        SeasonIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<Season>> GetSeasonById(
        string seasonId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SeasonByIdRequest request = new(seasonId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Season>>> GetSeasonsByIds(
        IReadOnlyCollection<string> seasonIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SeasonsByIdsRequest request = new(seasonIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Season>>> GetSeasonsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SeasonsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/pvp/games

    public Task<Replica<HashSet<Game>>> GetGames(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        GamesRequest request = new()
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<string>>> GetGamesIndex(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        GamesIndexRequest request = new()
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<Game>> GetGameById(
        string gameId,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        GameByIdRequest request = new(gameId)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Game>>> GetGamesByIds(
        IReadOnlyCollection<string> gameIds,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        GamesByIdsRequest request = new(gameIds)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Game>>> GetGamesByPage(
        int pageIndex,
        int? pageSize = default,
        string? accessToken = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        GamesByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion
}
