using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Worlds.Http;
using JetBrains.Annotations;

namespace GW2SDK.Worlds
{
    [PublicAPI]
    public sealed class WorldService
    {
        private readonly HttpClient http;

        private readonly MissingMemberBehavior missingMemberBehavior;

        private readonly IWorldReader worldReader;

        public WorldService(
            HttpClient http,
            IWorldReader worldReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
            this.worldReader = worldReader ?? throw new ArgumentNullException(nameof(worldReader));
            this.missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<World>> GetWorlds(
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new WorldsRequest(language);
            return await http.GetResourcesSet(request, json => worldReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetWorldsIndex(CancellationToken cancellationToken = default)
        {
            var request = new WorldsIndexRequest();
            return await http.GetResourcesSet(request, json => worldReader.Id.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<World>> GetWorldById(
            int worldId,
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new WorldByIdRequest(worldId, language);
            return await http.GetResource(request, json => worldReader.Read(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<World>> GetWorldsByIds(
#if NET
            IReadOnlySet<int> worldIds,
#else
            IReadOnlyCollection<int> worldIds,
#endif
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new WorldsByIdsRequest(worldIds, language);
            return await http.GetResourcesSet(request, json => worldReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<World>> GetWorldsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new WorldsByPageRequest(pageIndex, pageSize, language);
            return await http.GetResourcesPage(request, json => worldReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
