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
        private readonly IContinentReader continentReader;

        private readonly HttpClient http;

        private readonly MissingMemberBehavior missingMemberBehavior;

        public ContinentService(
            HttpClient http,
            IContinentReader continentReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            this.http = http;
            this.continentReader = continentReader;
            this.missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<Continent>> GetContinents(Language? language = default)
        {
            var request = new ContinentsRequest(language);
            return await http
                .GetResourcesSet(request, json => continentReader.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetContinentsIndex()
        {
            var request = new ContinentsIndexRequest();
            return await http
                .GetResourcesSet(request, json => continentReader.Id.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Continent>> GetContinentById(int continentId, Language? language = default)
        {
            var request = new ContinentByIdRequest(continentId, language);
            return await http.GetResource(request, json => continentReader.Read(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Continent>> GetContinentsByIds(
            IReadOnlyCollection<int> continentIds,
            Language? language = default
        )
        {
            var request = new ContinentsByIdsRequest(continentIds, language);
            return await http
                .GetResourcesSet(request, json => continentReader.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Continent>> GetContinentsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default
        )
        {
            var request = new ContinentsByPageRequest(pageIndex, pageSize, language);
            return await http
                .GetResourcesPage(request, json => continentReader.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Floor>> GetFloors(int continentId, Language? language = default)
        {
            var request = new FloorsRequest(continentId, language);
            return await http.GetResourcesSet(request,
                    json => continentReader.Floor.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetFloorsIndex(int continentId)
        {
            var request = new FloorsIndexRequest(continentId);
            return await http.GetResourcesSet(request,
                    json => continentReader.Floor.Id.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Floor>> GetFloorById(
            int continentId,
            int floorId,
            Language? language = default
        )
        {
            var request = new FloorByIdRequest(continentId, floorId, language);
            return await http.GetResource(request, json => continentReader.Floor.Read(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Floor>> GetFloorsByIds(
            int continentId,
            IReadOnlyCollection<int> floorIds,
            Language? language = default
        )
        {
            var request = new FloorsByIdsRequest(continentId, floorIds, language);
            return await http.GetResourcesSet(request,
                    json => continentReader.Floor.ReadArray(json, missingMemberBehavior))
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
            return await http.GetResourcesPage(request,
                    json => continentReader.Floor.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
