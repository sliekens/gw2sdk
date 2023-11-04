using GuildWars2.Home.Cats;
using GuildWars2.Home.Http;
using GuildWars2.Home.Nodes;

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

    public Task<(HashSet<Cat> Value, MessageContext Context)> GetCats(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        CatsRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetCatsIndex(CancellationToken cancellationToken = default)
    {
        CatsIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(Cat Value, MessageContext Context)> GetCatById(
        int catId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        CatByIdRequest request = new(catId) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<Cat> Value, MessageContext Context)> GetCatsByIds(
        IReadOnlyCollection<int> catIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        CatsByIdsRequest request = new(catIds) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
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
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetOwnedCatsIndex(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        OwnedCatsIndexRequest request = new()
        {
            AccessToken = accessToken
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<Node> Value, MessageContext Context)> GetNodes(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        NodesRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<string> Value, MessageContext Context)> GetNodesIndex(
        CancellationToken cancellationToken = default
    )
    {
        NodesIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<string> Value, MessageContext Context)> GetOwnedNodesIndex(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        OwnedNodesIndexRequest request = new()
        {
            AccessToken = accessToken
        };
        return request.SendAsync(http, cancellationToken);
    }
}
