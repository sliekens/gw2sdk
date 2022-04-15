using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Http;
using GW2SDK.Json;
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

#if NET
    public async Task<IReplica<IReadOnlySet<string>>> GetWorldBosses(CancellationToken cancellationToken = default)
#else
    public async Task<IReplica<IReadOnlyCollection<string>>> GetWorldBosses(
        CancellationToken cancellationToken = default
    )
#endif
    {
        WorldBossesRequest request = new();
        return await http.GetResourcesSetSimple(request, json => json.RootElement.GetStringArray(), cancellationToken)
            .ConfigureAwait(false);
    }

    [Scope(Permission.Progression)]
#if NET
    public async Task<IReplica<IReadOnlySet<string>>> GetWorldBossesOnCooldown(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
#else
    public async Task<IReplica<IReadOnlyCollection<string>>> GetWorldBossesOnCooldown(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
#endif
    {
        AccountWorldBossesRequest request = new(accessToken);
        return await http.GetResourcesSetSimple(request, json => json.RootElement.GetStringArray(), cancellationToken)
            .ConfigureAwait(false);
    }
}
