using GuildWars2.Pve.WorldBosses.Http;

namespace GuildWars2.Pve.WorldBosses;

/// <summary>Provides query methods for defeated world bosses.</summary>
[PublicAPI]
public sealed class WorldBossesClient
{
    private readonly HttpClient httpClient;

    public WorldBossesClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    public Task<(HashSet<string> Value, MessageContext Context)> GetWorldBosses(
        CancellationToken cancellationToken = default
    )
    {
        WorldBossesRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<string> Value, MessageContext Context)> GetDefeatedWorldBosses(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        DefeatedWorldBossesRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(httpClient, cancellationToken);
    }
}
