using System;
using System.Collections.Generic;
using System.Net.Http;
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

        private readonly MissingMemberBehavior missingMemberBehavior;

        private readonly IWorldBossReader worldBossReader;

        public WorldBossService(
            HttpClient http,
            IWorldBossReader worldBossReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
            this.worldBossReader = worldBossReader ?? throw new ArgumentNullException(nameof(worldBossReader));
            this.missingMemberBehavior = missingMemberBehavior;
        }

#if NET
        public async Task<IReplica<IReadOnlySet<string>>> GetWorldBosses()
#else
        public async Task<IReplica<IReadOnlyCollection<string>>> GetWorldBosses()
#endif
        {
            var request = new WorldBossesRequest();
            return await http.GetResourcesSetSimple(request,
                    json => worldBossReader.Id.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        [Scope(Permission.Progression)]
#if NET
        public async Task<IReplica<IReadOnlySet<string>>> GetWorldBossesOnCooldown(string? accessToken)
#else
        public async Task<IReplica<IReadOnlyCollection<string>>> GetWorldBossesOnCooldown(string? accessToken)
#endif
        {
            var request = new AccountWorldBossesRequest(accessToken);
            return await http.GetResourcesSetSimple(request,
                    json => worldBossReader.Id.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
