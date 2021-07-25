using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Continents.Http;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Continents
{
    // TODO: add direct lookups for regions, maps, ... https://github.com/arenanet/api-cdi/pull/2
    [PublicAPI]
    public sealed class ContinentService
    {
        private readonly IContinentReader _continentReader;

        private readonly HttpClient _http;

        private readonly MissingMemberBehavior _missingMemberBehavior;

        public ContinentService(
            HttpClient http,
            IContinentReader continentReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http;
            _continentReader = continentReader;
            _missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<Continent>> GetContinents(Language? language = default)
        {
            var request = new ContinentsRequest(language);
            return await _http
                .GetResourcesSet(request, json => _continentReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetContinentsIndex()
        {
            var request = new ContinentsIndexRequest();
            return await _http
                .GetResourcesSet(request, json => _continentReader.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Continent>> GetContinentById(int continentId, Language? language = default)
        {
            var request = new ContinentByIdRequest(continentId, language);
            return await _http.GetResource(request, json => _continentReader.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Continent>> GetContinentsByIds(
            IReadOnlyCollection<int> continentIds,
            Language? language = default
        )
        {
            var request = new ContinentsByIdsRequest(continentIds, language);
            return await _http
                .GetResourcesSet(request, json => _continentReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Continent>> GetContinentsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default
        )
        {
            var request = new ContinentsByPageRequest(pageIndex, pageSize, language);
            return await _http
                .GetResourcesPage(request, json => _continentReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Floor>> GetFloors(int continentId, Language? language = default)
        {
            var request = new FloorsRequest(continentId, language);
            return await _http.GetResourcesSet(request,
                    json => _continentReader.Floor.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetFloorsIndex(int continentId)
        {
            var request = new FloorsIndexRequest(continentId);
            return await _http.GetResourcesSet(request,
                    json => _continentReader.Floor.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Floor>> GetFloorById(
            int continentId,
            int floorId,
            Language? language = default
        )
        {
            var request = new FloorByIdRequest(continentId, floorId, language);
            return await _http.GetResource(request, json => _continentReader.Floor.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Floor>> GetFloorsByIds(
            int continentId,
            IReadOnlyCollection<int> floorIds,
            Language? language = default
        )
        {
            var request = new FloorsByIdsRequest(continentId, floorIds, language);
            return await _http.GetResourcesSet(request,
                    json => _continentReader.Floor.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Floor>> GetFloorsByPage(
            int continentId,
            int pageIndex,
            int? pageSize = default,
            Language? language = default
        )
        {
            var request = new FloorsByPageRequest(continentId, pageIndex, pageSize, language);
            return await _http.GetResourcesPage(request,
                    json => _continentReader.Floor.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
