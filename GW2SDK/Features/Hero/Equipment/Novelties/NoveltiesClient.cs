﻿using GuildWars2.Hero.Equipment.Novelties.Http;

namespace GuildWars2.Hero.Equipment.Novelties;

/// <summary>Provides query methods for novelties and novelties unlocked on the account.</summary>
[PublicAPI]
public sealed class NoveltiesClient
{
    private readonly HttpClient httpClient;

    public NoveltiesClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/novelties

    public Task<(HashSet<int> Value, MessageContext Context)> GetUnlockedNoveltiesIndex(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        UnlockedNoveltiesRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/account/novelties

    #region v2/novelties

    public Task<(HashSet<Novelty> Value, MessageContext Context)> GetNovelties(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        NoveltiesRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetNoveltiesIndex(
        CancellationToken cancellationToken = default
    )
    {
        NoveltiesIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(Novelty Value, MessageContext Context)> GetNoveltyById(
        int noveltyId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        NoveltyByIdRequest request = new(noveltyId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Novelty> Value, MessageContext Context)> GetNoveltiesByIds(
        IReadOnlyCollection<int> noveltyIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        NoveltiesByIdsRequest request = new(noveltyIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Novelty> Value, MessageContext Context)> GetNoveltiesByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        NoveltiesByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/novelties
}
