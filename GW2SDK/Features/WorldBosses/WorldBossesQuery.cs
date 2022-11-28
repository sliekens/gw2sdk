using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Annotations;
using JetBrains.Annotations;

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

    public Task<IReplicaSet<string>> GetWorldBosses(CancellationToken cancellationToken = default)
    {
        WorldBossesRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    [Scope(Permission.Progression)]
    public Task<IReplica<IReadOnlyCollection<string>>> GetDefeatedWorldBosses(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        DefeatedWorldBossesRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }
}
