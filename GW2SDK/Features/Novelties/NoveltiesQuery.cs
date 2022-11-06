using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace GW2SDK.Novelties;

[PublicAPI]
public sealed class NoveltiesQuery
{
    private readonly HttpClient http;

    public NoveltiesQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    public Task<IReplicaSet<Novelty>> GetNovelties(
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
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<int>> GetNoveltiesIndex(CancellationToken cancellationToken = default)
    {
        NoveltiesIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<Novelty>> GetNoveltyById(
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
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<Novelty>> GetNoveltiesByIds(
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
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaPage<Novelty>> GetNoveltiesByPage(
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
        return request.SendAsync(http, cancellationToken);
    }
}
