﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Accounts.WorldBosses.Http;
using GW2SDK.Annotations;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.WorldBosses
{
    [PublicAPI]
    public sealed class WorldBossService
    {
        private readonly HttpClient http;

        public WorldBossService(HttpClient http)
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
        }

#if NET
        public async Task<IReplica<IReadOnlySet<string>>> GetWorldBosses(CancellationToken cancellationToken = default)
#else
        public async Task<IReplica<IReadOnlyCollection<string>>> GetWorldBosses(
            CancellationToken cancellationToken = default
        )
#endif
        {
            var request = new WorldBossesRequest();
            return await http.GetResourcesSetSimple(request,
                    json => json.RootElement.GetStringArray(),
                    cancellationToken)
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
            var request = new AccountWorldBossesRequest(accessToken);
            return await http.GetResourcesSetSimple(request,
                    json => json.RootElement.GetStringArray(),
                    cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
