namespace GuildWars2.MapChests;

[PublicAPI]
public sealed class MapChestsQuery
{
    private readonly HttpClient http;

    public MapChestsQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/mapchests

    public Task<Replica<HashSet<string>>> GetReceivedMapChests(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        var request = new ReceivedMapChestsRequests { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/mapchests

    public Task<Replica<HashSet<string>>> GetMapChestsIndex(
        CancellationToken cancellationToken = default
    )
    {
        MapChestsIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<MapChest>> GetMapChestById(
        string mapChestId,
        CancellationToken cancellationToken = default
    )
    {
        MapChestByIdRequest request = new(mapChestId)
        {
            MissingMemberBehavior = MissingMemberBehavior.Error
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<MapChest>>> GetMapChestsByIds(
        IReadOnlyCollection<string> mapChestIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MapChestsByIdsRequest request = new(mapChestIds)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<MapChest>>> GetMapChestsByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MapChestsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            MissingMemberBehavior = missingMemberBehavior
        };

        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<MapChest>>> GetMapChests(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MapChestsRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion
}
