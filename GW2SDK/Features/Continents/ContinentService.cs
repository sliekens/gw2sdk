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

        public ContinentService(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IDataTransferCollection<Continent>> GetContinents(JsonSerializerSettings? settings = null)
        {
            using var request = new GetContinentsRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Continent>(context.ResultCount);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferCollection<Continent>(list, context);
        }

        public async Task<IDataTransferCollection<int>> GetContinentsIndex(JsonSerializerSettings? settings = null)
        {
            using var request = new GetContinentsIndexRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<int>(context.ResultCount);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferCollection<int>(list, context);
        }

        public async Task<Continent?> GetContinentById(int continentId, JsonSerializerSettings? settings = null)
        {
            using var request = new GetContinentByIdRequest.Builder(continentId).GetRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<Continent>(json, settings ?? Json.DefaultJsonSerializerSettings);
        }

        public async Task<IDataTransferCollection<Continent>> GetContinentsByIds(
            IReadOnlyCollection<int> continentIds,
            JsonSerializerSettings? settings = null)
        {
            if (continentIds == null)
            {
                throw new ArgumentNullException(nameof(continentIds));
            }

            if (continentIds.Count == 0)
            {
                throw new ArgumentException("Continent IDs cannot be an empty collection.", nameof(continentIds));
            }

            using var request = new GetContinentsByIdsRequest.Builder(continentIds).GetRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Continent>(context.ResultCount);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferCollection<Continent>(list, context);
        }

        public async Task<IDataTransferPage<Continent>> GetContinentsByPage(
            int pageIndex,
            int? pageSize = null,
            JsonSerializerSettings? settings = null)
        {
            using var request = new GetContinentsByPageRequest.Builder(pageIndex, pageSize).GetRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<Continent>(pageContext.PageSize);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferPage<Continent>(list, pageContext);
        }

        public async Task<IDataTransferCollection<Floor>> GetFloors(int continentId, JsonSerializerSettings? settings = null)
        {
            using var request = new GetFloorsRequest(continentId);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Floor>(context.ResultCount);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferCollection<Floor>(list, context);
        }

        public async Task<IDataTransferCollection<int>> GetFloorsIndex(int continentId, JsonSerializerSettings? settings = null)
        {
            using var request = new GetFloorsIndexRequest(continentId);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<int>(context.ResultCount);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferCollection<int>(list, context);
        }

        public async Task<Floor?> GetFloorById(int continentId, int floorId, JsonSerializerSettings? settings = null)
        {
            using var request = new GetFloorByIdRequest.Builder(continentId, floorId).GetRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<Floor>(json, settings ?? Json.DefaultJsonSerializerSettings);
        }

        public async Task<IDataTransferCollection<Floor>> GetFloorsByIds(int continentId, IReadOnlyCollection<int> floorIds, JsonSerializerSettings? settings = null)
        {
            if (floorIds == null)
            {
                throw new ArgumentNullException(nameof(floorIds));
            }

            if (floorIds.Count == 0)
            {
                throw new ArgumentException("Floor IDs cannot be an empty collection.", nameof(floorIds));
            }

            using var request = new GetFloorsByIdsRequest.Builder(continentId, floorIds).GetRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Floor>(context.ResultCount);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferCollection<Floor>(list, context);
        }

        public async Task<IDataTransferPage<Floor>> GetFloorsByPage(int continentId, int pageIndex, int? pageSize = null, JsonSerializerSettings? settings = null)
        {
            using var request = new GetFloorsByPageRequest.Builder(continentId, pageIndex, pageSize).GetRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<Floor>(pageContext.PageSize);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferPage<Floor>(list, pageContext);
        }
    }
}
