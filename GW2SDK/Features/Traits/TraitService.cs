using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;
using GW2SDK.Http;
using GW2SDK.Traits.Http;

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

        public async Task<IDataTransferCollection<Trait>> GetTraits()
        {
            var request = new TraitsRequest();
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Trait>(context.ResultCount);
            list.AddRange(_traitReader.ReadArray(json));
            return new DataTransferCollection<Trait>(list, context);
        }

        public async Task<IDataTransferCollection<int>> GetTraitsIndex()
        {
            var request = new TraitsIndexRequest();
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<int>(context.ResultCount);
            list.AddRange(_traitReader.Id.ReadArray(json));
            return new DataTransferCollection<int>(list, context);
        }

        public async Task<Trait?> GetTraitById(int traitId)
        {
            var request = new TraitByIdRequest(traitId);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            return _traitReader.Read(json);
        }

        public async Task<IDataTransferCollection<Trait>> GetTraitsByIds(IReadOnlyCollection<int> traitIds)
        {
            if (traitIds is null)
            {
                throw new ArgumentNullException(nameof(traitIds));
            }

            if (traitIds.Count == 0)
            {
                throw new ArgumentException("Trait IDs cannot be an empty collection.", nameof(traitIds));
            }

            var request = new TraitsByIdsRequest(traitIds);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Trait>(context.ResultCount);
            list.AddRange(_traitReader.ReadArray(json));
            return new DataTransferCollection<Trait>(list, context);
        }

        public async Task<IDataTransferPage<Trait>> GetTraitsByPage(int pageIndex, int? pageSize = null)
        {
            var request = new TraitsByPageRequest(pageIndex, pageSize);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<Trait>(pageContext.PageSize);
            list.AddRange(_traitReader.ReadArray(json));
            return new DataTransferPage<Trait>(list, pageContext);
        }
    }
}
