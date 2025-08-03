using System.Text.Json;

using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Inventories;

/// <summary>Provides query methods for bags and the shared inventory.</summary>
[PublicAPI]
public sealed class InventoryClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="InventoryClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public InventoryClient(HttpClient httpClient)
    {
        ThrowHelper.ThrowIfNull(httpClient);
        this.httpClient = httpClient;
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/inventory

    /// <summary>Retrieves the shared inventory of the account.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(Inventory Value, MessageContext Context)> GetSharedInventory(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/account/inventory", accessToken);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            Inventory value = response.Json.RootElement.GetInventory();
            return (value, response.Context);
        }
    }

    #endregion v2/account/inventory

    #region v2/characters/:id/inventory

    /// <summary>Retrieves the bags equipped by a character on the account.</summary>
    /// <param name="characterName">A character name that belongs to the account associated with the access token.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(Baggage Value, MessageContext Context)> GetInventory(
        string characterName,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet(
            $"v2/characters/{characterName}/inventory",
            accessToken
        );
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            Baggage value = response.Json.RootElement.GetBaggage();
            return (value, response.Context);
        }
    }

    #endregion v2/characters/:id/inventory
}
