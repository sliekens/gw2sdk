﻿using GuildWars2.Pvp.Amulets;
using GuildWars2.Pvp.Amulets.Http;
using GuildWars2.Pvp.Games;
using GuildWars2.Pvp.Games.Http;
using GuildWars2.Pvp.MistChampions;
using GuildWars2.Pvp.MistChampions.Http;
using GuildWars2.Pvp.Ranks;
using GuildWars2.Pvp.Ranks.Http;
using GuildWars2.Pvp.Seasons;
using GuildWars2.Pvp.Seasons.Http;
using GuildWars2.Pvp.Standings;
using GuildWars2.Pvp.Standings.Http;
using GuildWars2.Pvp.Stats;
using GuildWars2.Pvp.Stats.Http;

namespace GuildWars2.Pvp;

/// <summary>Provides query methods for PvP matches, seasons, rank, leaderboards, equipment and mist chanpions.</summary>
[PublicAPI]
public sealed class PvpClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="PvpClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public PvpClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/pvp/seasons/:id/leaderboards

    public Task<(HashSet<string> Value, MessageContext Context)> GetLeaderboards(
        string seasonId,
        CancellationToken cancellationToken = default
    )
    {
        LeaderboardsRequest request = new(seasonId);
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/pvp/seasons/:id/leaderboards/:board

    public Task<(HashSet<string> Value, MessageContext Context)> GetLeaderboardRegions(
        string seasonId,
        string boardId,
        CancellationToken cancellationToken = default
    )
    {
        LeaderboardRegionsRequest request = new(seasonId, boardId);
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/pvp/seasons/:id/leaderboards/:board/:region

    public Task<(HashSet<LeaderboardEntry> Value, MessageContext Context)> GetLeaderboardEntries(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/pvp/standings

    public Task<(HashSet<Standing> Value, MessageContext Context)> GetStandings(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/pvp/stats

    public Task<(AccountStats Value, MessageContext Context)> GetStats(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/account/pvp/heroes

    public Task<(HashSet<int> Value, MessageContext Context)> GetUnlockedMistChampions(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        UnlockedMistChampionsRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/account/pvp/heroes

    #region v2/pvp/amulets

    public Task<(HashSet<Amulet> Value, MessageContext Context)> GetAmulets(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetAmuletsIndex(
        CancellationToken cancellationToken = default
    )
    {
        AmuletIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(Amulet Value, MessageContext Context)> GetAmuletById(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Amulet> Value, MessageContext Context)> GetAmuletsByIds(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Amulet> Value, MessageContext Context)> GetAmuletsByPage(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/pvp/heroes

    public Task<(HashSet<MistChampion> Value, MessageContext Context)> GetMistChampions(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MistChampionsRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<string> Value, MessageContext Context)> GetMistChampionsIndex(
        CancellationToken cancellationToken = default
    )
    {
        MistChampionsIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(MistChampion Value, MessageContext Context)> GetMistChampionById(
        string mistChampionId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MistChampionByIdRequest request = new(mistChampionId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<MistChampion> Value, MessageContext Context)> GetMistChampionsByIds(
        IReadOnlyCollection<string> mistChampionIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MistChampionsByIdsRequest request = new(mistChampionIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<MistChampion> Value, MessageContext Context)> GetMistChampionByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MistChampionsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/pvp/ranks

    public Task<(HashSet<Rank> Value, MessageContext Context)> GetRanks(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetRanksIndex(
        CancellationToken cancellationToken = default
    )
    {
        RankIndexRequest request = new();
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

    #region v2/pvp/seasons

    public Task<(HashSet<Season> Value, MessageContext Context)> GetSeasons(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<string> Value, MessageContext Context)> GetSeasonsIndex(
        CancellationToken cancellationToken = default
    )
    {
        SeasonIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(Season Value, MessageContext Context)> GetSeasonById(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Season> Value, MessageContext Context)> GetSeasonsByIds(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Season> Value, MessageContext Context)> GetSeasonsByPage(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/pvp/games

    public Task<(HashSet<Game> Value, MessageContext Context)> GetGames(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<string> Value, MessageContext Context)> GetGamesIndex(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(Game Value, MessageContext Context)> GetGameById(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Game> Value, MessageContext Context)> GetGamesByIds(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Game> Value, MessageContext Context)> GetGamesByPage(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion
}
