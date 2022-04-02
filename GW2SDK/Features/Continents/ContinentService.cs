using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Continents.Http;
using GW2SDK.Continents.Json;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Continents
{
    // TODO: add direct lookups for regions, maps, ... https://github.com/arenanet/api-cdi/pull/2
    [PublicAPI]
    public sealed class ContinentService
    {
        private readonly HttpClient http;

        public ContinentService(HttpClient http)
        {
            this.http = http;
        }

        #region /v2/continents

        public async Task<IReplicaSet<Continent>> GetContinents(
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new ContinentsRequest(language);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => ContinentReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetContinentsIndex(CancellationToken cancellationToken = default)
        {
            var request = new ContinentsIndexRequest();
            return await http.GetResourcesSet(request, json => json.RootElement.GetInt32Array(), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Continent>> GetContinentById(
            int continentId,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new ContinentByIdRequest(continentId, language);
            return await http.GetResource(request,
                    json => ContinentReader.Read(json.RootElement, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Continent>> GetContinentsByIds(
#if NET
            IReadOnlySet<int> continentIds,
#else
            IReadOnlyCollection<int> continentIds,
#endif
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new ContinentsByIdsRequest(continentIds, language);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => ContinentReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Continent>> GetContinentsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new ContinentsByPageRequest(pageIndex, pageSize, language);
            return await http.GetResourcesPage(request,
                    json => json.RootElement.GetArray(item => ContinentReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        #endregion

        #region /v2/continents/:id/floors

        public async Task<IReplicaSet<Floor>> GetFloors(
            int continentId,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new FloorsRequest(continentId, language);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => FloorReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetFloorsIndex(
            int continentId,
            CancellationToken cancellationToken = default
        )
        {
            var request = new FloorsIndexRequest(continentId);
            return await http.GetResourcesSet(request, json => json.RootElement.GetInt32Array(), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Floor>> GetFloorById(
            int continentId,
            int floorId,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new FloorByIdRequest(continentId, floorId, language);
            return await http.GetResource(request,
                    json => FloorReader.Read(json.RootElement, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Floor>> GetFloorsByIds(
            int continentId,
#if NET
            IReadOnlySet<int> floorIds,
#else
            IReadOnlyCollection<int> floorIds,
#endif
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new FloorsByIdsRequest(continentId, floorIds, language);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => FloorReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Floor>> GetFloorsByPage(
            int continentId,
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new FloorsByPageRequest(continentId, pageIndex, pageSize, language);
            return await http.GetResourcesPage(request,
                    json => json.RootElement.GetArray(item => FloorReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        #endregion
    }
}
