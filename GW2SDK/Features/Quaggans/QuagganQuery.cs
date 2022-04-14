using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Quaggans.Http;
using GW2SDK.Quaggans.Json;
using GW2SDK.Quaggans.Models;

using JetBrains.Annotations;

namespace GW2SDK.Quaggans;

[PublicAPI]
public sealed class QuagganQuery
{
    private readonly HttpClient http;

    public QuagganQuery(HttpClient http)
    {
        this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
    }

    public async Task<IReplicaSet<Quaggan>> GetQuaggans(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        QuaggansRequest request = new();
        return await http.GetResourcesSet(request,
                json => json.RootElement.GetArray(item => QuagganReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaSet<string>> GetQuaggansIndex(CancellationToken cancellationToken = default)
    {
        QuaggansIndexRequest request = new();
        return await http.GetResourcesSet(request, json => json.RootElement.GetStringArray(), cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplica<Quaggan>> GetQuagganById(
        string quagganId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        QuagganByIdRequest request = new(quagganId);
        return await http.GetResource(request,
                json => QuagganReader.Read(json.RootElement, missingMemberBehavior),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaSet<Quaggan>> GetQuaggansByIds(
#if NET
        IReadOnlySet<string> quagganIds,
#else
        IReadOnlyCollection<string> quagganIds,
#endif
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        QuaggansByIdsRequest request = new(quagganIds);
        return await http.GetResourcesSet(request,
                json => json.RootElement.GetArray(item => QuagganReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaPage<Quaggan>> GetQuaggansByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        QuaggansByPageRequest request = new(pageIndex, pageSize);
        return await http.GetResourcesPage(request,
                json => json.RootElement.GetArray(item => QuagganReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }
}