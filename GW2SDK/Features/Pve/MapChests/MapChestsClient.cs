using GuildWars2.Pve.MapChests.Http;

namespace GuildWars2.Pve.MapChests;

/// <summary>Provides query methods for daily map rewards.</summary>
[PublicAPI]
public sealed class MapChestsClient
{
    private readonly HttpClient httpClient;

    public MapChestsClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/mapchests

    public Task<(HashSet<string> Value, MessageContext Context)> GetReceivedMapChests(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        var request = new ReceivedMapChestsRequest { AccessToken = accessToken };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/mapchests

    public Task<(HashSet<string> Value, MessageContext Context)> GetMapChestsIndex(
        CancellationToken cancellationToken = default
    )
    {
        MapChestsIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(MapChest Value, MessageContext Context)> GetMapChestById(
        string mapChestId,
        CancellationToken cancellationToken = default
    )
    {
        MapChestByIdRequest request = new(mapChestId)
        {
            MissingMemberBehavior = MissingMemberBehavior.Error
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<MapChest> Value, MessageContext Context)> GetMapChestsByIds(
        IReadOnlyCollection<string> mapChestIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MapChestsByIdsRequest request = new(mapChestIds)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<MapChest> Value, MessageContext Context)> GetMapChestsByPage(
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

        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<MapChest> Value, MessageContext Context)> GetMapChests(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MapChestsRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion
}
