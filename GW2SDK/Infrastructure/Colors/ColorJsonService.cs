using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Colors;

namespace GW2SDK.Infrastructure.Colors
{
    public sealed class ColorJsonService : IColorJsonService
    {
        private readonly HttpClient _http;

        public ColorJsonService([NotNull] HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<HttpResponseMessage> GetColorIds()
        {
            var resource = new UriBuilder(_http.BaseAddress)
            {
                Path = "/v2/colors"
            };
            return await _http.GetAsync(resource.Uri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> GetAllColors()
        {
            var resource = new UriBuilder(_http.BaseAddress)
            {
                Path = "/v2/colors",
                Query = "ids=all"
            };
            return await _http.GetAsync(resource.Uri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> GetColorById(int colorId)
        {
            var resource = new UriBuilder(_http.BaseAddress)
            {
                Path = "/v2/colors",
                Query = $"id={colorId}"
            };
            return await _http.GetAsync(resource.Uri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> GetColorsById(IReadOnlyList<int> colorIds)
        {
            var resource = new UriBuilder(_http.BaseAddress)
            {
                Path = "/v2/colors",
                Query = $"ids={colorIds.ToCsv()}"
            };
            return await _http.GetAsync(resource.Uri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> GetColorsPage(int page, int? pageSize)
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

            return await _http.GetAsync(resource.Uri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
        }
    }
}
