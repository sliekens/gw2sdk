﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace GuildWars2.BuildStorage;

[PublicAPI]
public sealed record BuildStorageQuery
{
    private readonly HttpClient http;

    public BuildStorageQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    public Task<IReplicaSet<Build>> GetBuildStorage(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        BuildStorageRequest request = new()
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<int>> GetBuildStorageIndex(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        BuildStorageIndexRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<Build>> GetBuildStorageSpaceById(
        string? accessToken,
        int buildStorageId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        BuildByIdRequest request = new(buildStorageId)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<Build>> GetBuildStorageSpacesByIds(
        string? accessToken,
        IReadOnlyCollection<int> buildStorageIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        BuildsByIdsRequest request = new(buildStorageIds)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaPage<Build>> GetBuildStorageSpacesByPage(
        string? accessToken,
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        BuildsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }
}
