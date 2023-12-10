﻿using GuildWars2.Hero.Equipment.Miniatures.Http;

namespace GuildWars2.Hero.Equipment.Miniatures;

/// <summary>Provides query methods for miniatures and miniatures unlocked on the account.</summary>
[PublicAPI]
public sealed class MiniaturesClient
{
    private readonly HttpClient httpClient;

    public MiniaturesClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/minis

    public Task<(HashSet<int> Value, MessageContext Context)> GetUnlockedMinipets(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        var request = new UnlockedMinipetsRequest { AccessToken = accessToken };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/minis

    public Task<(HashSet<Minipet> Value, MessageContext Context)> GetMinipets(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MinipetsRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetMinipetsIndex(
        CancellationToken cancellationToken = default
    )
    {
        MinipetsIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(Minipet Value, MessageContext Context)> GetMinipetById(
        int minipetId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MinipetByIdRequest request = new(minipetId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Minipet> Value, MessageContext Context)> GetMinipetsByIds(
        IReadOnlyCollection<int> minipetIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MinipetsByIdsRequest request = new(minipetIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Minipet> Value, MessageContext Context)> GetMinipetsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MinipetsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion
}