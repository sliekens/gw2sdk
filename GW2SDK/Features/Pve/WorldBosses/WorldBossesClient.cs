using GuildWars2.Pve.WorldBosses.Http;

namespace GuildWars2.Pve.WorldBosses;

/// <summary>Provides query methods for defeated world bosses.</summary>
[PublicAPI]
public sealed class WorldBossesClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="WorldBossesClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public WorldBossesClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    /// <summary>Retrieves the IDs of all world bosses.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<string> Value, MessageContext Context)> GetWorldBosses(
        CancellationToken cancellationToken = default
    )
    {
        WorldBossesRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all world bosses that have been defeated by the account since the last server reset.
    /// This endpoint is only accessible with a valid access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<string> Value, MessageContext Context)> GetDefeatedWorldBosses(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        DefeatedWorldBossesRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(httpClient, cancellationToken);
    }
}
