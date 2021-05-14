using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;
using GW2SDK.Http;
using GW2SDK.Worlds.Http;

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

        public async Task<IDataTransferCollection<World>> GetWorlds()
        {
            var request = new WorldsRequest();
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<World>();
            list.AddRange(_worldReader.ReadArray(json));
            return new DataTransferCollection<World>(list, context);
        }

        public async Task<IDataTransferCollection<int>> GetWorldsIndex()
        {
            var request = new WorldsIndexRequest();
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<int>(context.ResultCount);
            list.AddRange(_worldReader.Id.ReadArray(json));
            return new DataTransferCollection<int>(list, context);
        }

        public async Task<World> GetWorldById(int worldId)
        {
            var request = new WorldByIdRequest(worldId);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            return _worldReader.Read(json);
        }

        public async Task<IDataTransferCollection<World>> GetWorldsByIds(IReadOnlyCollection<int> worldIds)
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
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<World>(context.ResultCount);
            list.AddRange(_worldReader.ReadArray(json));
            return new DataTransferCollection<World>(list, context);
        }

        public async Task<IDataTransferPage<World>> GetWorldsByPage(int pageIndex, int? pageSize = null)
        {
            var request = new WorldsByPageRequest(pageIndex, pageSize);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<World>(pageContext.PageSize);
            list.AddRange(_worldReader.ReadArray(json));
            return new DataTransferPage<World>(list, pageContext);
        }
    }
}
