using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;
using GW2SDK.Continents.Http;
using GW2SDK.Http;

namespace GW2SDK.Continents
{
    // TODO: add direct lookups for regions, maps, ... https://github.com/arenanet/api-cdi/pull/2
    [PublicAPI]
    public sealed class ContinentService
    {
        private readonly HttpClient _http;

        private readonly IContinentReader _continentReader;

        public ContinentService(HttpClient http, IContinentReader continentReader)
        {
            _http = http;
            _continentReader = continentReader;
        }

        public async Task<IDataTransferCollection<Continent>> GetContinents()
        {
            var request = new ContinentsRequest();
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Continent>(context.ResultCount);
            list.AddRange(_continentReader.ReadArray(json));
            return new DataTransferCollection<Continent>(list, context);
        }

        public async Task<IDataTransferCollection<int>> GetContinentsIndex()
        {
            var request = new ContinentsIndexRequest();
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<int>(context.ResultCount);
            list.AddRange(_continentReader.Id.ReadArray(json));
            return new DataTransferCollection<int>(list, context);
        }

        public async Task<Continent> GetContinentById(int continentId)
        {
            var request = new ContinentByIdRequest(continentId);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            return _continentReader.Read(json);
        }

        public async Task<IDataTransferCollection<Continent>> GetContinentsByIds(IReadOnlyCollection<int> continentIds)
        {
            if (continentIds is null)
            {
                throw new ArgumentNullException(nameof(continentIds));
            }

            if (continentIds.Count == 0)
            {
                throw new ArgumentException("Continent IDs cannot be an empty collection.", nameof(continentIds));
            }

            var request = new ContinentsByIdsRequest(continentIds);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Continent>(context.ResultCount);
            list.AddRange(_continentReader.ReadArray(json));
            return new DataTransferCollection<Continent>(list, context);
        }

        public async Task<IDataTransferPage<Continent>> GetContinentsByPage(int pageIndex, int? pageSize = null)
        {
            var request = new ContinentsByPageRequest(pageIndex, pageSize);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<Continent>(pageContext.PageSize);
            list.AddRange(_continentReader.ReadArray(json));
            return new DataTransferPage<Continent>(list, pageContext);
        }

        public async Task<IDataTransferCollection<Floor>> GetFloors(int continentId)
        {
            var request = new FloorsRequest(continentId);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Floor>(context.ResultCount);
            list.AddRange(_continentReader.Floor.ReadArray(json));
            return new DataTransferCollection<Floor>(list, context);
        }

        public async Task<IDataTransferCollection<int>> GetFloorsIndex(int continentId)
        {
            var request = new FloorsIndexRequest(continentId);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<int>(context.ResultCount);
            list.AddRange(_continentReader.Floor.Id.ReadArray(json));
            return new DataTransferCollection<int>(list, context);
        }

        public async Task<Floor> GetFloorById(int continentId, int floorId)
        {
            var request = new FloorByIdRequest(continentId, floorId);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            return _continentReader.Floor.Read(json);
        }

        public async Task<IDataTransferCollection<Floor>> GetFloorsByIds(int continentId, IReadOnlyCollection<int> floorIds)
        {
            if (floorIds is null)
            {
                throw new ArgumentNullException(nameof(floorIds));
            }

            if (floorIds.Count == 0)
            {
                throw new ArgumentException("Floor IDs cannot be an empty collection.", nameof(floorIds));
            }

            var request = new FloorsByIdsRequest(continentId, floorIds);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Floor>(context.ResultCount);
            list.AddRange(_continentReader.Floor.ReadArray(json));
            return new DataTransferCollection<Floor>(list, context);
        }

        public async Task<IDataTransferPage<Floor>> GetFloorsByPage(int continentId, int pageIndex, int? pageSize = null)
        {
            var request = new FloorsByPageRequest(continentId, pageIndex, pageSize);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<Floor>(pageContext.PageSize);
            list.AddRange(_continentReader.Floor.ReadArray(json));
            return new DataTransferPage<Floor>(list, pageContext);
        }
    }
}
