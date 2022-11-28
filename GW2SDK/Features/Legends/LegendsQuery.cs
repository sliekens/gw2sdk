using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace GuildWars2.Legends;

[PublicAPI]
public sealed class LegendsQuery
{
    private readonly HttpClient http;

    public LegendsQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    public Task<IReplicaSet<string>> GetLegendsIndex(CancellationToken cancellationToken = default)
    {
        LegendsIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<Legend>> GetLegendById(
        string legendId,
        CancellationToken cancellationToken = default
    )
    {
        LegendByIdRequest request = new(legendId)
        {
            MissingMemberBehavior = MissingMemberBehavior.Error
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<Legend>> GetLegendsByIds(
        IReadOnlyCollection<string> legendIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        LegendsByIdsRequest request = new(legendIds)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaPage<Legend>> GetLegendsByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        LegendsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            MissingMemberBehavior = missingMemberBehavior
        };

        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<Legend>> GetLegends(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        LegendsRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }
}
