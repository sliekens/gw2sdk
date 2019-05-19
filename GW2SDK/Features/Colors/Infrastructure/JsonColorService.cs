using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
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

        public async Task<(string Json, Dictionary<string, string> MetaData)> GetColorIds()
        {
            var resource = new UriBuilder(_http.BaseAddress)
            {
                Path = "/v2/colors"
            };
            return await _http.GetStringWithMetaDataAsync(resource.Uri).ConfigureAwait(false);
        }

        public async Task<(string Json, Dictionary<string, string> MetaData)> GetAllColors()
        {
            var resource = new UriBuilder(_http.BaseAddress)
            {
                Path = "/v2/colors",
                Query = "ids=all"
            };
            return await _http.GetStringWithMetaDataAsync(resource.Uri).ConfigureAwait(false);
        }

        public async Task<(string Json, Dictionary<string, string> MetaData)> GetColorById(int colorId)
        {
            var resource = new UriBuilder(_http.BaseAddress)
            {
                Path = "/v2/colors",
                Query = $"id={colorId}"
            };
            return await _http.GetStringWithMetaDataAsync(resource.Uri).ConfigureAwait(false);
        }

        public async Task<(string Json, Dictionary<string, string> MetaData)> GetColorsById(IReadOnlyList<int> colorIds)
        {
            var resource = new UriBuilder(_http.BaseAddress)
            {
                Path = "/v2/colors",
                Query = $"ids={colorIds.ToCsv()}"
            };
            return await _http.GetStringWithMetaDataAsync(resource.Uri).ConfigureAwait(false);
        }

        public async Task<(string Json, Dictionary<string, string> MetaData)> GetColorsPage(int page, int? pageSize)
        {
            var resource = new UriBuilder(_http.BaseAddress)
            {
                Path = "/v2/colors",
                Query = $"page={page}"
            };

            if (pageSize.HasValue)
            {
                resource.Query += $"&page_size={pageSize}";
            }

            return await _http.GetStringWithMetaDataAsync(resource.Uri).ConfigureAwait(false);
        }
    }
}
