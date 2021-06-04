using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Traits.Http;
using JetBrains.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    public sealed class TraitService
    {
        private readonly HttpClient _http;
        private readonly ITraitReader _traitReader;

        public TraitService(HttpClient http, ITraitReader traitReader)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _traitReader = traitReader ?? throw new ArgumentNullException(nameof(traitReader));
        }

        public async Task<IDataTransferSet<Trait>> GetTraits()
        {
            var request = new TraitsRequest();
            return await _http.GetResourcesSet(request, json => _traitReader.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferSet<int>> GetTraitsIndex()
        {
            var request = new TraitsIndexRequest();
            return await _http.GetResourcesSet(request, json => _traitReader.Id.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<Trait> GetTraitById(int traitId)
        {
            var request = new TraitByIdRequest(traitId);
            return await _http.GetResource(request, json => _traitReader.Read(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferSet<Trait>> GetTraitsByIds(IReadOnlyCollection<int> traitIds)
        {
            var request = new TraitsByIdsRequest(traitIds);
            return await _http.GetResourcesSet(request, json => _traitReader.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferPage<Trait>> GetTraitsByPage(int pageIndex, int? pageSize = null)
        {
            var request = new TraitsByPageRequest(pageIndex, pageSize);
            return await _http.GetResourcesPage(request, json => _traitReader.ReadArray(json))
                .ConfigureAwait(false);
        }
    }
}
