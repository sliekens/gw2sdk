using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Worlds.Http;
using JetBrains.Annotations;

namespace GW2SDK.Worlds
{
    [PublicAPI]
    public sealed class WorldService
    {
        private readonly HttpClient _http;

        private readonly IWorldReader _worldReader;

        public WorldService(HttpClient http, IWorldReader worldReader)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _worldReader = worldReader ?? throw new ArgumentNullException(nameof(worldReader));
        }

        public async Task<IDataTransferSet<World>> GetWorlds()
        {
            var request = new WorldsRequest();
            return await _http.GetResourcesSet(request, json => _worldReader.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferSet<int>> GetWorldsIndex()
        {
            var request = new WorldsIndexRequest();
            return await _http.GetResourcesSet(request, json => _worldReader.Id.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<World> GetWorldById(int worldId)
        {
            var request = new WorldByIdRequest(worldId);
            return await _http.GetResource(request, json => _worldReader.Read(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferSet<World>> GetWorldsByIds(IReadOnlyCollection<int> worldIds)
        {
            if (worldIds is null)
            {
                throw new ArgumentNullException(nameof(worldIds));
            }

            if (worldIds.Count == 0)
            {
                throw new ArgumentException("World IDs cannot be an empty collection.", nameof(worldIds));
            }

            var request = new WorldsByIdsRequest(worldIds);
            return await _http.GetResourcesSet(request, json => _worldReader.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferPage<World>> GetWorldsByPage(int pageIndex, int? pageSize = null)
        {
            var request = new WorldsByPageRequest(pageIndex, pageSize);
            return await _http.GetResourcesPage(request, json => _worldReader.ReadArray(json))
                .ConfigureAwait(false);
        }
    }
}
