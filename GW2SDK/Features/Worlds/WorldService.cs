using System;
using System.Collections.Generic;
using System.Net.Http;
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
        private readonly HttpClient _http;

        private readonly MissingMemberBehavior _missingMemberBehavior;

        private readonly IWorldReader _worldReader;

        public WorldService(
            HttpClient http,
            IWorldReader worldReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _worldReader = worldReader ?? throw new ArgumentNullException(nameof(worldReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<World>> GetWorlds(Language? language = default)
        {
            var request = new WorldsRequest(language);
            return await _http.GetResourcesSet(request, json => _worldReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetWorldsIndex()
        {
            var request = new WorldsIndexRequest();
            return await _http.GetResourcesSet(request, json => _worldReader.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<World>> GetWorldById(int worldId, Language? language = default)
        {
            var request = new WorldByIdRequest(worldId, language);
            return await _http.GetResource(request, json => _worldReader.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<World>> GetWorldsByIds(
            IReadOnlyCollection<int> worldIds,
            Language? language = default
        )
        {
            var request = new WorldsByIdsRequest(worldIds, language);
            return await _http.GetResourcesSet(request, json => _worldReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<World>> GetWorldsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default
        )
        {
            var request = new WorldsByPageRequest(pageIndex, pageSize, language);
            return await _http.GetResourcesPage(request, json => _worldReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
