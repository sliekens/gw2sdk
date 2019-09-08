using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Extensions;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Worlds.Impl;
using Newtonsoft.Json;

namespace GW2SDK.Worlds
{
    [PublicAPI]
    public sealed class WorldService
    {
        private readonly HttpClient _http;

        public WorldService([NotNull] HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IDataTransferList<World>> GetWorlds([CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetWorldsRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var listContext = response.Headers.GetListContext();
                var list = new List<World>();
                JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
                return new DataTransferList<World>(list, listContext);
            }
        }

        public async Task<IDataTransferList<int>> GetWorldsIndex([CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetWorldsIndexRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var listContext = response.Headers.GetListContext();
                var list = new List<int>(listContext.ResultCount);
                JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
                return new DataTransferList<int>(list, listContext);
            }
        }

        public async Task<World> GetWorldById(int worldId, [CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetWorldByIdRequest.Builder(worldId).GetRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<World>(json, settings ?? Json.DefaultJsonSerializerSettings);
            }
        }

        public async Task<IDataTransferList<World>> GetWorldsByIds([NotNull] IReadOnlyList<int> worldIds, [CanBeNull] JsonSerializerSettings settings = null)
        {
            if (worldIds == null)
            {
                throw new ArgumentNullException(nameof(worldIds));
            }

            if (worldIds.Count == 0)
            {
                throw new ArgumentException("World IDs cannot be an empty collection.", nameof(worldIds));
            }

            using (var request = new GetWorldsByIdsRequest.Builder(worldIds).GetRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var listContext = response.Headers.GetListContext();
                var list = new List<World>(listContext.ResultCount);
                JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
                return new DataTransferList<World>(list, listContext);
            }
        }

        public async Task<IDataTransferPage<World>> GetWorldsByPage(int pageIndex, int? pageSize = null, [CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetWorldsByPageRequest.Builder(pageIndex, pageSize).GetRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var pageContext = response.Headers.GetPageContext();
                var list = new List<World>(pageContext.PageSize);
                JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
                return new DataTransferPage<World>(list, pageContext);
            }
        }
    }
}
