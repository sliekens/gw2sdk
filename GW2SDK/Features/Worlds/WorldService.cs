using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Worlds.Http;
using GW2SDK.Worlds.Json;
using JetBrains.Annotations;

namespace GW2SDK.Worlds
{
    [PublicAPI]
    public sealed class WorldService
    {
        private readonly HttpClient http;

        public WorldService(HttpClient http)
        {
            this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IReplicaSet<World>> GetWorlds(
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new WorldsRequest(language);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => WorldReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetWorldsIndex(CancellationToken cancellationToken = default)
        {
            var request = new WorldsIndexRequest();
            return await http.GetResourcesSet(request, json => json.RootElement.GetInt32Array(), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<World>> GetWorldById(
            int worldId,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new WorldByIdRequest(worldId, language);
            return await http.GetResource(request,
                    json => WorldReader.Read(json.RootElement, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<World>> GetWorldsByIds(
#if NET
            IReadOnlySet<int> worldIds,
#else
            IReadOnlyCollection<int> worldIds,
#endif
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new WorldsByIdsRequest(worldIds, language);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => WorldReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<World>> GetWorldsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new WorldsByPageRequest(pageIndex, pageSize, language);
            return await http.GetResourcesPage(request,
                    json => json.RootElement.GetArray(item => WorldReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
