using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Home.Cats.Http;
using GW2SDK.Home.Cats.Models;
using GW2SDK.Home.Nodes.Http;
using GW2SDK.Home.Nodes.Models;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Home;

[PublicAPI]
public sealed class HomeQuery
{
    private readonly HttpClient http;

    public HomeQuery(HttpClient http)
    {
        this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
    }

    public Task<IReplicaSet<Cat>> GetCats(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        CatsRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<int>> GetCatsIndex(CancellationToken cancellationToken = default)
    {
        CatsIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<Cat>> GetCatById(
        int catId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        CatByIdRequest request = new(catId) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<Cat>> GetCatsByIds(
        IReadOnlyCollection<int> catIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        CatsByIdsRequest request = new(catIds) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaPage<Cat>> GetCatsByPage(
        int pageIndex,
        int? pageSize,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        CatsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<IReadOnlyCollection<int>>> GetOwnedCatsIndex(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        OwnedCatsIndexRequest request = new(accessToken);
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<Node>> GetNodes(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        NodesRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<string>> GetNodesIndex(CancellationToken cancellationToken = default)
    {
        NodesIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<IReadOnlyCollection<string>>> GetOwnedNodesIndex(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        OwnedNodesRequest request = new(accessToken);
        return request.SendAsync(http, cancellationToken);
    }
}
