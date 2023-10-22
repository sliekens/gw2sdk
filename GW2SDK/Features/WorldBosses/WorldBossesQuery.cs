using GuildWars2.WorldBosses.Http;

namespace GuildWars2.WorldBosses;

[PublicAPI]
public sealed class WorldBossesQuery
{
    private readonly HttpClient http;

    public WorldBossesQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    public Task<Replica<HashSet<string>>> GetWorldBosses(
        CancellationToken cancellationToken = default
    )
    {
        WorldBossesRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<string>>> GetDefeatedWorldBosses(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        DefeatedWorldBossesRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }
}
