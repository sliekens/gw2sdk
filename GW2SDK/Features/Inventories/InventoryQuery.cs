using GuildWars2.Inventories.Http;

namespace GuildWars2.Inventories;

/// <summary>Query methods for bags and the shared inventory.</summary>
[PublicAPI]
public sealed class InventoryQuery
{
    private readonly HttpClient http;

    public InventoryQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/inventory

    /// <summary>Retrieves the shared inventory of the account.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<Replica<Inventory>> GetSharedInventory(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SharedInventoryRequest request = new()
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/account/inventory

    #region v2/characters/:id/inventory

    /// <summary>Retrieves the bags equipped by a character on the account.</summary>
    /// <param name="characterName">A character name that belongs to the account associated with the access token.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<Replica<Baggage>> GetInventory(
        string characterName,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        InventoryRequest request = new(characterName)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/characters/:id/inventory
}
