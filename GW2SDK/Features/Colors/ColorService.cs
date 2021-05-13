using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;
using GW2SDK.Colors.Http;
using GW2SDK.Http;

namespace GW2SDK.Colors
{
    [PublicAPI]
    public sealed class ColorService
    {
        private readonly IColorReader _colorReader;

        private readonly HttpClient _http;

        public ColorService(HttpClient http, IColorReader colorReader)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _colorReader = colorReader ?? throw new ArgumentNullException(nameof(colorReader));
        }

        public async Task<IDataTransferCollection<Color>> GetColors()
        {
            var request = new ColorsRequest();
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Color>(context.ResultCount);
            list.AddRange(_colorReader.ReadArray(json));
            return new DataTransferCollection<Color>(list, context);
        }

        public async Task<IDataTransferCollection<int>> GetColorsIndex()
        {
            var request = new ColorsIndexRequest();
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<int>(context.ResultCount);
            list.AddRange(_colorReader.Id.ReadArray(json));
            return new DataTransferCollection<int>(list, context);
        }

        public async Task<Color?> GetColorById(int colorId)
        {
            var request = new ColorByIdRequest(colorId);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            return _colorReader.Read(json);
        }

        public async Task<IDataTransferCollection<Color>> GetColorsByIds(IReadOnlyCollection<int> colorIds)
        {
            if (colorIds is null)
            {
                throw new ArgumentNullException(nameof(colorIds));
            }

            if (colorIds.Count == 0)
            {
                throw new ArgumentException("Color IDs cannot be an empty collection.", nameof(colorIds));
            }

            var request = new ColorsByIdsRequest(colorIds);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Color>(context.ResultCount);
            list.AddRange(_colorReader.ReadArray(json));
            return new DataTransferCollection<Color>(list, context);
        }

        public async Task<IDataTransferPage<Color>> GetColorsByPage(int pageIndex, int? pageSize = null)
        {
            var request = new ColorsByPageRequest(pageIndex, pageSize);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<Color>(pageContext.PageSize);
            list.AddRange(_colorReader.ReadArray(json));
            return new DataTransferPage<Color>(list, pageContext);
        }
    }
}
