using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting.Daily;

/// <summary>Provides query methods for items which can be crafted once per day.</summary>
[PublicAPI]
public sealed class DailyCraftingClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="DailyCraftingClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public DailyCraftingClient(HttpClient httpClient)
    {
        ThrowHelper.ThrowIfNull(httpClient);
        this.httpClient = httpClient;
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/dailycrafting

    /// <summary>Retrieves items that can be crafted once per day per account.</summary>
    /// <remarks>The returned values are strings such as "charged_quartz_crystal" instead of the item ID, so you will need to
    /// maintain a conversion from the strings to the item IDs to get the item details.</remarks>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<string> Value, MessageContext Context)> GetDailyCraftableItems(
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/dailycrafting");
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            var value = response.Json.RootElement.GetSet(static entry => entry.GetStringRequired());
            return (value, response.Context);
        }
    }

    #endregion

    #region v2/account/dailycrafting

    /// <summary>Retrieves the items that were crafted today by the account associated with the provided access token. This
    /// endpoint is only accessible with a valid access token.</summary>
    /// <remarks>The returned values are strings such as "charged_quartz_crystal" instead of the item ID, so you will need to
    /// maintain a conversion from the strings to the item IDs to get the item details.</remarks>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<string> Value, MessageContext Context)> GetDailyCraftedItems(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/account/dailycrafting", accessToken);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            var value = response.Json.RootElement.GetSet(static entry => entry.GetStringRequired());
            return (value, response.Context);
        }
    }

    #endregion
}
