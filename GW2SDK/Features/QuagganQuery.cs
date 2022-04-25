using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Json;
using GW2SDK.Quaggans;
using JetBrains.Annotations;

namespace GW2SDK;

[PublicAPI]
public sealed class QuagganQuery
{
    private readonly HttpClient http;

    public QuagganQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    public Task<IReplicaSet<Quaggan>> GetQuaggans(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        QuaggansRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<string>> GetQuaggansIndex(CancellationToken cancellationToken = default)
    {
        QuaggansIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<Quaggan>> GetQuagganById(
        string quagganId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        QuagganByIdRequest request = new(quagganId)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<Quaggan>> GetQuaggansByIds(
        IReadOnlyCollection<string> quagganIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        QuaggansByIdsRequest request =
            new(quagganIds) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaPage<Quaggan>> GetQuaggansByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        QuaggansByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }
}
