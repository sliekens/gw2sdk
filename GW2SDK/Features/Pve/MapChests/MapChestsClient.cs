using GuildWars2.Pve.MapChests.Http;

namespace GuildWars2.Pve.MapChests;

/// <summary>Provides query methods for daily map rewards.</summary>
[PublicAPI]
public sealed class MapChestsClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="MapChestsClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public MapChestsClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/mapchests

    /// <summary>Retrieves the IDs of all map chests that have been received since the last server reset on the account
    /// associated with the access token. This endpoint is only accessible with a valid access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
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

    /// <summary>Retrieves the IDs of all map chests.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<string> Value, MessageContext Context)> GetMapChestsIndex(
        CancellationToken cancellationToken = default
    )
    {
        MapChestsIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a map chest by its ID.</summary>
    /// <param name="mapChestId">The map chest ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(MapChest Value, MessageContext Context)> GetMapChestById(
        string mapChestId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MapChestByIdRequest request = new(mapChestId)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves map chests by their IDs.</summary>
    /// <param name="mapChestIds">The map chest IDs.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<MapChest> Value, MessageContext Context)> GetMapChestsByIds(
        IEnumerable<string> mapChestIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MapChestsByIdsRequest request = new(mapChestIds.ToList())
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of map chests.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
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

    /// <summary>Retrieves all map chests.</summary>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
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
