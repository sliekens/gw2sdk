using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Traits.Http;
using JetBrains.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    public sealed class TraitService
    {
        private readonly HttpClient _http;

        private readonly MissingMemberBehavior _missingMemberBehavior;

        private readonly ITraitReader _traitReader;

        public TraitService(
            HttpClient http,
            ITraitReader traitReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _traitReader = traitReader ?? throw new ArgumentNullException(nameof(traitReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<Trait>> GetTraits(Language? language = default)
        {
            var request = new TraitsRequest(language);
            return await _http.GetResourcesSet(request, json => _traitReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetTraitsIndex()
        {
            var request = new TraitsIndexRequest();
            return await _http.GetResourcesSet(request, json => _traitReader.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Trait>> GetTraitById(int traitId, Language? language = default)
        {
            var request = new TraitByIdRequest(traitId, language);
            return await _http.GetResource(request, json => _traitReader.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Trait>> GetTraitsByIds(
            IReadOnlyCollection<int> traitIds,
            Language? language = default
        )
        {
            var request = new TraitsByIdsRequest(traitIds, language);
            return await _http.GetResourcesSet(request, json => _traitReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Trait>> GetTraitsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default
        )
        {
            var request = new TraitsByPageRequest(pageIndex, pageSize, language);
            return await _http.GetResourcesPage(request, json => _traitReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
