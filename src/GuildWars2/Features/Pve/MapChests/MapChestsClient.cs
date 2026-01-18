using System.Text.Json;

using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Pve.MapChests;

/// <summary>Provides query methods for daily map rewards.</summary>
public sealed class MapChestsClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="MapChestsClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public MapChestsClient(HttpClient httpClient)
    {
        ThrowHelper.ThrowIfNull(httpClient);
        this.httpClient = httpClient;
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/mapchests

    /// <summary>Retrieves the IDs of all map chests that have been received since the last server reset on the account
    /// associated with the access token. This endpoint is only accessible with a valid access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<string> Value, MessageContext Context)> GetReceivedMapChests(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/account/mapchests", accessToken);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            IImmutableValueSet<string> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetStringRequired());
            return (value, response.Context);
        }
    }

    #endregion v2/account/mapchests

    #region v2/mapchests

    /// <summary>Retrieves the IDs of all map chests.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<string> Value, MessageContext Context)> GetMapChestsIndex(
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/mapchests");
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            IImmutableValueSet<string> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetStringRequired());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a map chest by its ID.</summary>
    /// <param name="mapChestId">The map chest ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(MapChest Value, MessageContext Context)> GetMapChestById(
        string mapChestId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/mapchests");
        requestBuilder.Query.AddId(mapChestId);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            MapChest value = response.Json.RootElement.GetMapChest();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves map chests by their IDs.</summary>
    /// <param name="mapChestIds">The map chest IDs.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<MapChest> Value, MessageContext Context)> GetMapChestsByIds(
        IEnumerable<string> mapChestIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/mapchests");
        requestBuilder.Query.AddIds(mapChestIds);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            IImmutableValueSet<MapChest> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetMapChest());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of map chests.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<MapChest> Value, MessageContext Context)> GetMapChestsByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/mapchests");
        requestBuilder.Query.AddPage(pageIndex, pageSize);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            IImmutableValueSet<MapChest> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetMapChest());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves all map chests.</summary>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<MapChest> Value, MessageContext Context)> GetMapChests(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/mapchests");
        requestBuilder.Query.AddAllIds();
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            IImmutableValueSet<MapChest> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetMapChest());
            return (value, response.Context);
        }
    }

    #endregion v2/mapchests
}
