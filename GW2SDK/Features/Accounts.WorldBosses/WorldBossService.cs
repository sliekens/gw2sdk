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
        private readonly HttpClient _http;

        private readonly MissingMemberBehavior _missingMemberBehavior;

        private readonly IWorldBossReader _worldBossReader;

        public WorldBossService(
            HttpClient http,
            IWorldBossReader worldBossReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _worldBossReader = worldBossReader ?? throw new ArgumentNullException(nameof(worldBossReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

#if NET
        public async Task<IReplica<IReadOnlySet<string>>> GetWorldBosses()
#else
        public async Task<IReplica<ISet<string>>> GetWorldBosses()
#endif
        {
            var request = new WorldBossesRequest();
            return await _http.GetResourcesSetSimple(request,
                    json => _worldBossReader.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        [Scope(Permission.Progression)]
#if NET
        public async Task<IReplica<IReadOnlySet<string>>> GetWorldBossesOnCooldown(string? accessToken)
#else
        public async Task<IReplica<ISet<string>>> GetWorldBossesOnCooldown(string? accessToken)
#endif
        {
            var request = new AccountWorldBossesRequest(accessToken);
            return await _http.GetResourcesSetSimple(request,
                    json => _worldBossReader.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
