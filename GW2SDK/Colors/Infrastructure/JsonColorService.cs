using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GW2SDK.Colors.Infrastructure
{
    public class JsonColorService : IJsonColorService
    {
        private readonly HttpClient _http;

        public JsonColorService(HttpClient http)
        {
            _http = http;
        }

        public async Task<string> GetAllColors()
        {
            return await _http.GetStringAsync("/v2/colors?ids=all");
        }

        public async Task<string> GetColorIds()
        {
            return await _http.GetStringAsync("/v2/colors");
        }

        public async Task<string> GetColorById(int colorId)
        {
            var search = $"id={colorId}";
            return await _http.GetStringAsync($"/v2/colors?{search}");
        }

        public async Task<string> GetColorsById(IReadOnlyList<int> colorIds)
        {
            var ids = string.Join(",", colorIds);
            var search = $"ids={ids}";
            var resource = $"/v2/colors?{search}";
            return await _http.GetStringAsync(resource);
        }

        public async Task<string> GetColorsPage(int page, int? pageSize)
        {
            var search = $"page=${page}";
            if (pageSize.HasValue)
            {
                search += $"&page_size={pageSize}";
            }
            return await _http.GetStringAsync($"/v2/colors?${search}");
        }
    }
}
