using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Colors.Impl;
using GW2SDK.Extensions;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonConverters;
using Newtonsoft.Json;

namespace GW2SDK.Colors
{
    [PublicAPI]
    public sealed class ColorService
    {
        private readonly HttpClient _http;

        public ColorService(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IDataTransferCollection<Color>> GetColors(JsonSerializerSettings? settings = null)
        {
            using var request = new GetColorsRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Color>(context.ResultCount);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferCollection<Color>(list, context);
        }

        public async Task<IDataTransferCollection<int>> GetColorsIndex(JsonSerializerSettings? settings = null)
        {
            using var request = new GetColorsIndexRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<int>(context.ResultCount);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferCollection<int>(list, context);
        }

        public async Task<Color?> GetColorById(int colorId, JsonSerializerSettings? settings = null)
        {
            using var request = new GetColorByIdRequest.Builder(colorId).GetRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<Color>(json, settings ?? Json.DefaultJsonSerializerSettings);
        }

        public async Task<IDataTransferCollection<Color>> GetColorsByIds(IReadOnlyCollection<int> colorIds, JsonSerializerSettings? settings = null)
        {
            if (colorIds == null)
            {
                throw new ArgumentNullException(nameof(colorIds));
            }

            if (colorIds.Count == 0)
            {
                throw new ArgumentException("Color IDs cannot be an empty collection.", nameof(colorIds));
            }

            using var request = new GetColorsByIdsRequest.Builder(colorIds).GetRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Color>(context.ResultCount);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferCollection<Color>(list, context);
        }

        public async Task<IDataTransferPage<Color>> GetColorsByPage(int pageIndex, int? pageSize = null, JsonSerializerSettings? settings = null)
        {
            using var request = new GetColorsByPageRequest.Builder(pageIndex, pageSize).GetRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<Color>(pageContext.PageSize);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferPage<Color>(list, pageContext);
        }
    }
}
