using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Http;
using GW2SDK.Titles.Http;

namespace GW2SDK.Titles
{
    [PublicAPI]
    public sealed class TitleService
    {
        private readonly HttpClient _http;

        private readonly ITitleReader _titleReader;

        public TitleService(HttpClient http, ITitleReader titleReader)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _titleReader = titleReader ?? throw new ArgumentNullException(nameof(titleReader));
        }

        public async Task<IDataTransferCollection<Title>> GetTitles()
        {
            var request = new TitlesRequest();
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Title>(context.ResultCount);
            list.AddRange(_titleReader.ReadArray(json));
            return new DataTransferCollection<Title>(list, context);
        }

        public async Task<IDataTransferCollection<int>> GetTitlesIndex()
        {
            var request = new TitlesIndexRequest();
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<int>(context.ResultCount);
            list.AddRange(_titleReader.Id.ReadArray(json));
            return new DataTransferCollection<int>(list, context);
        }

        public async Task<Title> GetTitleById(int titleId)
        {
            var request = new TitleByIdRequest(titleId);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            return _titleReader.Read(json);
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
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Title>(context.ResultCount);
            list.AddRange(_titleReader.ReadArray(json));
            return new DataTransferCollection<Title>(list, context);
        }

        public async Task<IDataTransferPage<Title>> GetTitlesByPage(int pageIndex, int? pageSize = null)
        {
            var request = new TitlesByPageRequest(pageIndex, pageSize);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<Title>(pageContext.PageSize);
            list.AddRange(_titleReader.ReadArray(json));
            return new DataTransferPage<Title>(list, pageContext);
        }
    }
}
