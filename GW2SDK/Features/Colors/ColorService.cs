using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Colors.Impl;
using GW2SDK.Http;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonReaders;

namespace GW2SDK.Colors
{
    [PublicAPI]
    public sealed class ColorService
    {
        private static readonly IJsonReader<int> KeyReader = new Int32JsonReader();
        private static readonly IJsonReader<IEnumerable<int>> KeyArrayReader = new JsonArrayReader<int>(KeyReader);
        private static readonly IJsonReader<Color> ValueReader = ColorJsonReader.Instance;
        private static readonly IJsonReader<IEnumerable<Color>> ValueArrayReader = new JsonArrayReader<Color>(ValueReader);

        private readonly HttpClient _http;

        public ColorService(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IDataTransferCollection<Color>> GetColors()
        {
            var request = new ColorsRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            await using var json = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            using var jsonDocument = await JsonDocument.ParseAsync(json).ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Color>(context.ResultCount);
            list.AddRange(ValueArrayReader.Read(jsonDocument));
            return new DataTransferCollection<Color>(list, context);
        }

        public async Task<IDataTransferCollection<int>> GetColorsIndex()
        {
            var request = new ColorsIndexRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            await using var json = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            using var jsonDocument = await JsonDocument.ParseAsync(json).ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<int>(context.ResultCount);
            list.AddRange(KeyArrayReader.Read(jsonDocument));
            return new DataTransferCollection<int>(list, context);
        }

        public async Task<Color?> GetColorById(int colorId)
        {
            var request = new ColorByIdRequest(colorId);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            await using var json = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            using var jsonDocument = await JsonDocument.ParseAsync(json).ConfigureAwait(false);
            return ValueReader.Read(jsonDocument);
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
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            await using var json = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            using var jsonDocument = await JsonDocument.ParseAsync(json).ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Color>(context.ResultCount);
            list.AddRange(ValueArrayReader.Read(jsonDocument));
            return new DataTransferCollection<Color>(list, context);
        }

        public async Task<IDataTransferPage<Color>> GetColorsByPage(int pageIndex, int? pageSize = null)
        {
            var request = new ColorsByPageRequest(pageIndex, pageSize);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            await using var json = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            using var jsonDocument = await JsonDocument.ParseAsync(json).ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<Color>(pageContext.PageSize);
            list.AddRange(ValueArrayReader.Read(jsonDocument));
            return new DataTransferPage<Color>(list, pageContext);
        }
    }
}
