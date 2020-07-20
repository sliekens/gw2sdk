using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Http;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonReaders;
using GW2SDK.Titles.Impl;

namespace GW2SDK.Titles
{
    [PublicAPI]
    public sealed class TitleService
    {
        private static readonly IJsonReader<int> KeyReader = new Int32JsonReader();
        private static readonly IJsonReader<IEnumerable<int>> KeyArrayReader = new JsonArrayReader<int>(KeyReader);
        private static readonly IJsonReader<Title> ValueReader = TitleJsonReader.Instance;
        private static readonly IJsonReader<IEnumerable<Title>> ValueArrayReader = new JsonArrayReader<Title>(ValueReader);

        private readonly HttpClient _http;

        public TitleService(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IDataTransferCollection<Title>> GetTitles()
        {
            var request = new TitlesRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            await using var json = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            using var jsonDocument = await JsonDocument.ParseAsync(json).ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Title>(context.ResultCount);
            list.AddRange(ValueArrayReader.Read(jsonDocument.RootElement));
            return new DataTransferCollection<Title>(list, context);
        }

        public async Task<IDataTransferCollection<int>> GetTitlesIndex()
        {
            var request = new TitlesIndexRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            await using var json = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            using var jsonDocument = await JsonDocument.ParseAsync(json).ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<int>(context.ResultCount);
            list.AddRange(KeyArrayReader.Read(jsonDocument.RootElement));
            return new DataTransferCollection<int>(list, context);
        }

        public async Task<Title?> GetTitleById(int titleId)
        {
            var request = new TitleByIdRequest(titleId);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            await using var json = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            using var jsonDocument = await JsonDocument.ParseAsync(json).ConfigureAwait(false);
            return ValueReader.Read(jsonDocument.RootElement);
        }

        public async Task<IDataTransferCollection<Title>> GetTitlesByIds(IReadOnlyCollection<int> titleIds)
        {
            if (titleIds is null)
            {
                throw new ArgumentNullException(nameof(titleIds));
            }

            if (titleIds.Count == 0)
            {
                throw new ArgumentException("Title IDs cannot be an empty collection.", nameof(titleIds));
            }

            var request = new TitlesByIdsRequest(titleIds);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            await using var json = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            using var jsonDocument = await JsonDocument.ParseAsync(json).ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Title>(context.ResultCount);
            list.AddRange(ValueArrayReader.Read(jsonDocument.RootElement));
            return new DataTransferCollection<Title>(list, context);
        }

        public async Task<IDataTransferPage<Title>> GetTitlesByPage(int pageIndex, int? pageSize = null)
        {
            var request = new TitlesByPageRequest(pageIndex, pageSize);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            await using var json = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            using var jsonDocument = await JsonDocument.ParseAsync(json).ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<Title>(pageContext.PageSize);
            list.AddRange(ValueArrayReader.Read(jsonDocument.RootElement));
            return new DataTransferPage<Title>(list, pageContext);
        }
    }
}
