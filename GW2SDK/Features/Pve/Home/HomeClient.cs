using GuildWars2.Pve.Home.Cats;
using GuildWars2.Pve.Home.Cats.Http;
using GuildWars2.Pve.Home.Nodes;
using GuildWars2.Pve.Home.Nodes.Http;

namespace GuildWars2.Pve.Home;

/// <summary>Provides query methods for unlocked Home nodes and cats.</summary>
[PublicAPI]
public sealed class HomeClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="HomeClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public HomeClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    public Task<(HashSet<Cat> Value, MessageContext Context)> GetCats(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        CatsRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetCatsIndex(
        CancellationToken cancellationToken = default
    )
    {
        CatsIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(Cat Value, MessageContext Context)> GetCatById(
        int catId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        CatByIdRequest request = new(catId) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Cat> Value, MessageContext Context)> GetCatsByIds(
        IReadOnlyCollection<int> catIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        CatsByIdsRequest request = new(catIds) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Cat> Value, MessageContext Context)> GetCatsByPage(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetUnlockedCats(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        UnlockedCatsIndexRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Node> Value, MessageContext Context)> GetNodes(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        NodesRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<string> Value, MessageContext Context)> GetNodesIndex(
        CancellationToken cancellationToken = default
    )
    {
        NodesIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }
    public Task<(Node Value, MessageContext Context)> GetNodeById(
        string nodeId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        NodeByIdRequest request = new(nodeId) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Node> Value, MessageContext Context)> GetNodesByIds(
        IReadOnlyCollection<string> nodeIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        NodesByIdsRequest request = new(nodeIds) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Node> Value, MessageContext Context)> GetNodesByPage(
        int pageIndex,
        int? pageSize,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        NodesByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<string> Value, MessageContext Context)> GetUnlockedNodes(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        UnlockedNodesIndexRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(httpClient, cancellationToken);
    }
}
