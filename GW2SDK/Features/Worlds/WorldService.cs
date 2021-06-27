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

        private readonly IWorldReader _worldReader;
        private readonly MissingMemberBehavior _missingMemberBehavior;

        public WorldService(HttpClient http, IWorldReader worldReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _worldReader = worldReader ?? throw new ArgumentNullException(nameof(worldReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IDataTransferSet<World>> GetWorlds()
        {
            var request = new WorldsRequest();
            return await _http.GetResourcesSet(request, json => _worldReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferSet<int>> GetWorldsIndex()
        {
            var request = new WorldsIndexRequest();
            return await _http.GetResourcesSet(request, json => _worldReader.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<World> GetWorldById(int worldId)
        {
            var request = new WorldByIdRequest(worldId);
            return await _http.GetResource(request, json => _worldReader.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferSet<World>> GetWorldsByIds(IReadOnlyCollection<int> worldIds)
        {
            var request = new WorldsByIdsRequest(worldIds);
            return await _http.GetResourcesSet(request, json => _worldReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferPage<World>> GetWorldsByPage(int pageIndex, int? pageSize = null)
        {
            var request = new WorldsByPageRequest(pageIndex, pageSize);
            return await _http.GetResourcesPage(request, json => _worldReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
