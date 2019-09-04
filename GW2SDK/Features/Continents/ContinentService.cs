using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Continents.Impl;
using GW2SDK.Extensions;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonConverters;
using Newtonsoft.Json;

namespace GW2SDK.Continents
{
    [PublicAPI]
    public sealed class ContinentService
    {
        private readonly HttpClient _http;

        public ContinentService([NotNull] HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IDataTransferList<Continent>> GetContinents([CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetContinentsRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var listContext = response.Headers.GetListContext();
                var list = new List<Continent>(listContext.ResultCount);
                JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
                return new DataTransferList<Continent>(list, listContext);
            }
        }

        public async Task<IDataTransferList<int>> GetContinentsIndex([CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetContinentsIndexRequest())
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

        public async Task<Continent> GetContinentById(int continentId, [CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetContinentByIdRequest.Builder(continentId).GetRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<Continent>(json, settings ?? Json.DefaultJsonSerializerSettings);
            }
        }

        public async Task<IDataTransferList<Continent>> GetContinentsByIds(
            [NotNull] IReadOnlyList<int> continentIds,
            [CanBeNull] JsonSerializerSettings settings = null)
        {
            if (continentIds == null)
            {
                throw new ArgumentNullException(nameof(continentIds));
            }

            if (continentIds.Count == 0)
            {
                throw new ArgumentException("Continent IDs cannot be an empty collection.", nameof(continentIds));
            }

            using (var request = new GetContinentsByIdsRequest.Builder(continentIds).GetRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var listContext = response.Headers.GetListContext();
                var list = new List<Continent>(listContext.ResultCount);
                JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
                return new DataTransferList<Continent>(list, listContext);
            }
        }

        public async Task<IDataTransferPage<Continent>> GetContinentsByPage(
            int pageIndex,
            int? pageSize = null,
            [CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetContinentsByPageRequest.Builder(pageIndex, pageSize).GetRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var pageContext = response.Headers.GetPageContext();
                var list = new List<Continent>(pageContext.PageSize);
                JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
                return new DataTransferPage<Continent>(list, pageContext);
            }
        }

        public async Task<IDataTransferList<Floor>> GetFloors(int continentId, [CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetFloorsRequest(continentId))
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var listContext = response.Headers.GetListContext();
                var list = new List<Floor>(listContext.ResultCount);
                JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
                return new DataTransferList<Floor>(list, listContext);
            }
        }

        public async Task<IDataTransferList<int>> GetFloorsIndex(int continentId, [CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetFloorsIndexRequest(continentId))
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

        public async Task<Floor> GetFloorById(int continentId, int floorId, [CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetFloorByIdRequest.Builder(continentId, floorId).GetRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<Floor>(json, settings ?? Json.DefaultJsonSerializerSettings);
            }
        }

        public async Task<IDataTransferList<Floor>> GetFloorsByIds(int continentId, [NotNull] IReadOnlyList<int> floorIds, [CanBeNull] JsonSerializerSettings settings = null)
        {
            if (floorIds == null)
            {
                throw new ArgumentNullException(nameof(floorIds));
            }

            if (floorIds.Count == 0)
            {
                throw new ArgumentException("Floor IDs cannot be an empty collection.", nameof(floorIds));
            }

            using (var request = new GetFloorsByIdsRequest.Builder(continentId, floorIds).GetRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var listContext = response.Headers.GetListContext();
                var list = new List<Floor>(listContext.ResultCount);
                JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
                return new DataTransferList<Floor>(list, listContext);
            }
        }

        public async Task<IDataTransferPage<Floor>> GetFloorsByPage(int continentId, int pageIndex, int? pageSize = null, [CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetFloorsByPageRequest.Builder(continentId, pageIndex, pageSize).GetRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var pageContext = response.Headers.GetPageContext();
                var list = new List<Floor>(pageContext.PageSize);
                JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
                return new DataTransferPage<Floor>(list, pageContext);
            }
        }
    }
}
