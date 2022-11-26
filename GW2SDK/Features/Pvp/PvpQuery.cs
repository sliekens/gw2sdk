using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Pvp.Amulets;
using JetBrains.Annotations;

namespace GW2SDK.Pvp;

[PublicAPI]
public sealed class PvpQuery
{
    private readonly HttpClient http;

    public PvpQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    public Task<IReplicaSet<Amulet>> GetAmulets(
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

    public Task<IReplicaSet<int>> GetAmuletsIndex(CancellationToken cancellationToken = default)
    {
        AmuletIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<Amulet>> GetAmuletById(
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

    public Task<IReplicaSet<Amulet>> GetAmuletsByIds(
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

    public Task<IReplicaPage<Amulet>> GetAmuletsByPage(
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
}
