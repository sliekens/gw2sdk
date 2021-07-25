using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Masteries.Http;
using JetBrains.Annotations;

namespace GW2SDK.Masteries
{
    [PublicAPI]
    public sealed class MasteryService
    {
        private readonly HttpClient _http;

        private readonly IMasteryReader _masteryReader;

        private readonly MissingMemberBehavior _missingMemberBehavior;

        public MasteryService(
            HttpClient http,
            IMasteryReader masteryReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _masteryReader = masteryReader ?? throw new ArgumentNullException(nameof(masteryReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<Mastery>> GetMasteries(Language? language = default)
        {
            var request = new MasteriesRequest(language);
            return await _http.GetResourcesSet(request, json => _masteryReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetMasteriesIndex()
        {
            var request = new MasteriesIndexRequest();
            return await _http
                .GetResourcesSet(request, json => _masteryReader.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Mastery>> GetMasteryById(int masteryId, Language? language = default)
        {
            var request = new MasteryByIdRequest(masteryId, language);
            return await _http.GetResource(request, json => _masteryReader.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Mastery>> GetMasteriesByIds(
            IReadOnlyCollection<int> masteryIds,
            Language? language = default
        )
        {
            var request = new MasteriesByIdsRequest(masteryIds, language);
            return await _http.GetResourcesSet(request, json => _masteryReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
