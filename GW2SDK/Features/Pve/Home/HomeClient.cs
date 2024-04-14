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

    #region v2/account/home/cats

    /// <summary>Retrieves the IDs of home cats unlocked on the account associated with the access token. This endpoint is only
    /// accessible with a valid access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<int> Value, MessageContext Context)> GetUnlockedCats(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        UnlockedCatsIndexRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/account/home/cats

    #region v2/account/home/nodes

    /// <summary>Retrieves the IDs of home gathering nodes unlocked on the account associated with the access token. This
    /// endpoint is only accessible with a valid access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<string> Value, MessageContext Context)> GetUnlockedNodes(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        UnlockedNodesIndexRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/account/home/nodes

    #region v2/home/cats

    /// <summary>Retrieves all home cats.</summary>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Cat> Value, MessageContext Context)> GetCats(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        CatsRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all home cats.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<int> Value, MessageContext Context)> GetCatsIndex(
        CancellationToken cancellationToken = default
    )
    {
        CatsIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a home cat by its ID.</summary>
    /// <param name="catId">The cat ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(Cat Value, MessageContext Context)> GetCatById(
        int catId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        CatByIdRequest request = new(catId) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves home cats by their IDs.</summary>
    /// <param name="catIds">The cat IDs.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Cat> Value, MessageContext Context)> GetCatsByIds(
        IEnumerable<int> catIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        CatsByIdsRequest request = new(catIds.ToList()) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of home cats.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
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

    #endregion v2/home/cats

    #region v2/home/nodes

    /// <summary>Retrieves all home gathering nodes.</summary>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Node> Value, MessageContext Context)> GetNodes(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        NodesRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all home gathering nodes.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<string> Value, MessageContext Context)> GetNodesIndex(
        CancellationToken cancellationToken = default
    )
    {
        NodesIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a home gathering node by its ID.</summary>
    /// <param name="nodeId">The node ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(Node Value, MessageContext Context)> GetNodeById(
        string nodeId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        NodeByIdRequest request = new(nodeId) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves home gathering nodes by their IDs.</summary>
    /// <param name="nodeIds">The node IDs.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Node> Value, MessageContext Context)> GetNodesByIds(
        IEnumerable<string> nodeIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        NodesByIdsRequest request = new(nodeIds.ToList()) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of home gathering nodes.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
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

    #endregion v2/home/nodes
}
