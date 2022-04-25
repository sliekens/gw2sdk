using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Accounts.WorldBosses;
using GW2SDK.Annotations;
using GW2SDK.WorldBosses;
using JetBrains.Annotations;

namespace GW2SDK;

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
