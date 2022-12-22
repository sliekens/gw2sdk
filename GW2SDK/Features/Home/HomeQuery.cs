using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Home.Cats;
using GuildWars2.Home.Nodes;
using JetBrains.Annotations;

namespace GuildWars2.Home;

[PublicAPI]
public sealed class HomeQuery
{
    private readonly HttpClient http;

    public HomeQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    public Task<Replica<HashSet<Cat>>> GetCats(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        CatsRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<int>>> GetCatsIndex(CancellationToken cancellationToken = default)
    {
        CatsIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<Cat>> GetCatById(
        int catId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        CatByIdRequest request = new(catId) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Cat>>> GetCatsByIds(
        IReadOnlyCollection<int> catIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        CatsByIdsRequest request = new(catIds) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Cat>>> GetCatsByPage(
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

    public Task<Replica<HashSet<int>>> GetOwnedCatsIndex(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        OwnedCatsIndexRequest request = new(accessToken);
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Node>>> GetNodes(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        NodesRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<string>>> GetNodesIndex(
        CancellationToken cancellationToken = default
    )
    {
        NodesIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<string>>> GetOwnedNodesIndex(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        OwnedNodesIndexRequest request = new(accessToken);
        return request.SendAsync(http, cancellationToken);
    }
}
