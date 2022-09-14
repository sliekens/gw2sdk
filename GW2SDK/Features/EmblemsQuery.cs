﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Emblems;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK;

[PublicAPI]
public sealed class EmblemsQuery
{
    private readonly HttpClient http;

    public EmblemsQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region /v2/emblem/foregrounds

    public Task<IReplicaSet<Emblem>> GetForegroundEmblems(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ForegroundEmblemsRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<int>> GetForegroundEmblemsIndex(
        CancellationToken cancellationToken = default
    )
    {
        var request = new ForegroundEmblemsIndexRequest();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<Emblem>> GetForegroundEmblemById(
        int foregroundEmblemId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ForegroundEmblemByIdRequest request =
            new(foregroundEmblemId) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<Emblem>> GetForegroundEmblemsByIds(
        IReadOnlyCollection<int> foregroundEmblemIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ForegroundEmblemsByIdsRequest request =
            new(foregroundEmblemIds) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaPage<Emblem>> GetForegroundEmblemsByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ForegroundEmblemsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region /v2/emblem/backgrounds

    public Task<IReplicaSet<Emblem>> GetBackgroundEmblems(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        BackgroundEmblemsRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<int>> GetBackgroundEmblemsIndex(
        CancellationToken cancellationToken = default
    )
    {
        var request = new BackgroundEmblemsIndexRequest();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<Emblem>> GetBackgroundEmblemById(
        int backgroundEmblemId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        BackgroundEmblemByIdRequest request =
            new(backgroundEmblemId) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<Emblem>> GetBackgroundEmblemsByIds(
        IReadOnlyCollection<int> backgroundEmblemIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        BackgroundEmblemsByIdsRequest request =
            new(backgroundEmblemIds) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaPage<Emblem>> GetBackgroundEmblemsByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        BackgroundEmblemsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion
}