using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Http;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonReaders;
using GW2SDK.Traits.Impl;

namespace GW2SDK.Traits
{
    [PublicAPI]
    public sealed class TraitService
    {
        private static readonly IJsonReader<int> KeyReader = new Int32JsonReader();
        private static readonly IJsonReader<IEnumerable<int>> KeyArrayReader = new JsonArrayReader<int>(KeyReader);
        private static readonly IJsonReader<Trait> ValueReader = TraitJsonReader.Instance;
        private static readonly IJsonReader<IEnumerable<Trait>> ValueArrayReader = new JsonArrayReader<Trait>(ValueReader);

        private readonly HttpClient _http;

        public TraitService(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IDataTransferCollection<Trait>> GetTraits()
        {
            var request = new TraitsRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            await using var json = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            using var jsonDocument = await JsonDocument.ParseAsync(json).ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Trait>(context.ResultCount);
            list.AddRange(ValueArrayReader.Read(jsonDocument.RootElement));
            return new DataTransferCollection<Trait>(list, context);
        }

        public async Task<IDataTransferCollection<int>> GetTraitsIndex()
        {
            var request = new TraitsIndexRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            await using var json = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            using var jsonDocument = await JsonDocument.ParseAsync(json).ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<int>(context.ResultCount);
            list.AddRange(KeyArrayReader.Read(jsonDocument.RootElement));
            return new DataTransferCollection<int>(list, context);
        }

        public async Task<Trait?> GetTraitById(int traitId)
        {
            var request = new TraitByIdRequest(traitId);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            await using var json = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            using var jsonDocument = await JsonDocument.ParseAsync(json).ConfigureAwait(false);
            return ValueReader.Read(jsonDocument.RootElement);
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
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            await using var json = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            using var jsonDocument = await JsonDocument.ParseAsync(json).ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Trait>(context.ResultCount);
            list.AddRange(ValueArrayReader.Read(jsonDocument.RootElement));
            return new DataTransferCollection<Trait>(list, context);
        }

        public async Task<IDataTransferPage<Trait>> GetTraitsByPage(int pageIndex, int? pageSize = null)
        {
            var request = new TraitsByPageRequest(pageIndex, pageSize);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            await using var json = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            using var jsonDocument = await JsonDocument.ParseAsync(json).ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<Trait>(pageContext.PageSize);
            list.AddRange(ValueArrayReader.Read(jsonDocument.RootElement));
            return new DataTransferPage<Trait>(list, pageContext);
        }
    }
}
