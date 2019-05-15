using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Colors.Infrastructure
{
    public sealed class JsonColorService : IJsonColorService
    {
        private readonly HttpClient _http;

        public JsonColorService([NotNull] HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
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
