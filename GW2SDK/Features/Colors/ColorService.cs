using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Common;
using GW2SDK.Infrastructure;
using GW2SDK.Infrastructure.Common;
using Newtonsoft.Json;

namespace GW2SDK.Features.Colors
{
    public sealed class ColorService
    {
        private readonly IColorJsonService _api;

        public ColorService([NotNull] IColorJsonService api)
        {
            _api = api ?? throw new ArgumentNullException(nameof(api));
        }

        public async Task<IDataTransferList<int>> GetColorIds([CanBeNull] JsonSerializerSettings settings = null)
        {
            var response = await _api.GetColorIds().ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var listContext = response.Headers.GetListContext();
            var list = new List<int>(listContext.ResultCount);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferList<int>(list, listContext);
        }

        public async Task<Color> GetColorById(int colorId, [CanBeNull] JsonSerializerSettings settings = null)
        {
            var response = await _api.GetColorById(colorId).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<Color>(json, settings ?? Json.DefaultJsonSerializerSettings);
        }

        public async Task<IDataTransferList<Color>> GetColorsById([NotNull] IReadOnlyList<int> colorIds,
            [CanBeNull] JsonSerializerSettings settings = null)
        {
            if (colorIds == null) throw new ArgumentNullException(nameof(colorIds));
            var response = await _api.GetColorsById(colorIds).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var listContext = response.Headers.GetListContext();
            var list = new List<Color>(listContext.ResultCount);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferList<Color>(list, listContext);
        }

        public async Task<IDataTransferList<Color>> GetAllColors([CanBeNull] JsonSerializerSettings settings = null)
        {
            var response = await _api.GetAllColors().ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var listContext = response.Headers.GetListContext();
            var list = new List<Color>(listContext.ResultCount);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferList<Color>(list, listContext);
        }

        public async Task<IDataTransferPage<Color>> GetColorsPage(int page, int? pageSize,
            [CanBeNull] JsonSerializerSettings settings = null)
        {
            var response = await _api.GetColorsPage(page, pageSize).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<Color>(pageContext.PageSize);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferPage<Color>(list, pageContext);
        }
    }
}
