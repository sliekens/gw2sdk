using GW2SDK.Colors.Infrastructure;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GW2SDK.Colors
{
    public class ColorService
    {
        private readonly JsonColorService _http;

        public ColorService(JsonColorService http)
        {
            _http = http;
        }

        public async Task<List<Color>> GetAllColors()
        {
            var json = await _http.GetAllColors();
            return JsonConvert.DeserializeObject<List<Color>>(json);
        }

        public async Task<List<int>> GetColorIds()
        {
            var json = await _http.GetColorIds();
            return JsonConvert.DeserializeObject<List<int>>(json);
        }

        public async Task<Color> GetColorById(int colorId)
        {
            var json = await _http.GetColorById(colorId);
            return JsonConvert.DeserializeObject<Color>(json);
        }

        public async Task<List<Color>> GetColorsById(IReadOnlyList<int> colorIds)
        {
            var json = await _http.GetColorsById(colorIds);
            return JsonConvert.DeserializeObject<List<Color>>(json);
        }

        public async Task<Color> GetColorsPage(int page, int? pageSize)
        {
            var json = await _http.GetColorsPage(page, pageSize);
            return JsonConvert.DeserializeObject<Color>(json);
        }
    }
}
