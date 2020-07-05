using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Http;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Titles.Impl;
using Newtonsoft.Json;

namespace GW2SDK.Titles
{
    [PublicAPI]
    public sealed class TitleService
    {
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
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Title>(context.ResultCount);
            JsonConvert.PopulateObject(json, list, Json.DefaultJsonSerializerSettings);
            return new DataTransferCollection<Title>(list, context);
        }

        public async Task<IDataTransferCollection<int>> GetTitlesIndex()
        {
            var request = new TitlesIndexRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<int>(context.ResultCount);
            JsonConvert.PopulateObject(json, list, Json.DefaultJsonSerializerSettings);
            return new DataTransferCollection<int>(list, context);
        }

        public async Task<Title?> GetTitleById(int titleId)
        {
            var request = new TitleByIdRequest(titleId);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<Title>(json, Json.DefaultJsonSerializerSettings);
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
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Title>(context.ResultCount);
            JsonConvert.PopulateObject(json, list, Json.DefaultJsonSerializerSettings);
            return new DataTransferCollection<Title>(list, context);
        }

        public async Task<IDataTransferPage<Title>> GetTitlesByPage(int pageIndex, int? pageSize = null)
        {
            var request = new TitlesByPageRequest(pageIndex, pageSize);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<Title>(pageContext.PageSize);
            JsonConvert.PopulateObject(json, list, Json.DefaultJsonSerializerSettings);
            return new DataTransferPage<Title>(list, pageContext);
        }
    }
}
