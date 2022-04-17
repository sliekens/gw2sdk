using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Http;
using GW2SDK.WorldBosses.Http;
using JetBrains.Annotations;

namespace GW2SDK.WorldBosses;

[PublicAPI]
public sealed class WorldBossQuery
{
    private readonly HttpClient http;

    public WorldBossQuery(HttpClient http)
    {
        this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
    }

    public Task<IReplicaSet<string>> GetWorldBosses(CancellationToken cancellationToken = default)
    {
        WorldBossesRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    [Scope(Permission.Progression)]
    public Task<IReplica<IReadOnlyCollection<string>>> GetWorldBossesOnCooldown(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        AccountWorldBossesRequest request = new()
        {
            AccessToken = accessToken
        };
        return request.SendAsync(http, cancellationToken);
    }
}
