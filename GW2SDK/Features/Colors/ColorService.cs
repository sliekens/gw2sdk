using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Features.Colors.Infrastructure;
using GW2SDK.Infrastructure;
using Newtonsoft.Json;

namespace GW2SDK.Features.Colors
{
    public sealed class ColorService
    {
        private readonly IJsonColorService _api;

        public ColorService([NotNull] IJsonColorService api)
        {
            _api = api ?? throw new ArgumentNullException(nameof(api));
        }

        public async Task<List<Color>> GetAllColors([CanBeNull] JsonSerializerSettings settings = null)
        {
            var json = await _api.GetAllColors();
            return JsonConvert.DeserializeObject<List<Color>>(json, settings);
        }

        public async Task<List<int>> GetColorIds([CanBeNull] JsonSerializerSettings settings = null)
        {
            var json = await _api.GetColorIds();
            return JsonConvert.DeserializeObject<List<int>>(json, settings);
        }

        public async Task<Color> GetColorById(int colorId, [CanBeNull] JsonSerializerSettings settings = null)
        {
            var json = await _api.GetColorById(colorId);
            return JsonConvert.DeserializeObject<Color>(json, settings);
        }

        public async Task<List<Color>> GetColorsById(IReadOnlyList<int> colorIds, [CanBeNull] JsonSerializerSettings settings = null)
        {
            var json = await _api.GetColorsById(colorIds);
            return JsonConvert.DeserializeObject<List<Color>>(json, settings);
        }

        public async Task<Color> GetColorsPage(int page, int? pageSize, [CanBeNull] JsonSerializerSettings settings = null)
        {
            var json = await _api.GetColorsPage(page, pageSize);
            return JsonConvert.DeserializeObject<Color>(json, settings);
        }
    }
}