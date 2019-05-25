using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
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
            using (var request = new GetColorIdsRequest())
            {
                return await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            }
        }

        public async Task<HttpResponseMessage> GetAllColors()
        {
            using (var request = new GetAllColorsRequest())
            {
                return await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            }
        }

        public async Task<HttpResponseMessage> GetColorById(int colorId)
        {
            using (var request = new GetColorByIdRequest(colorId))
            {
                return await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            }
        }

        public async Task<HttpResponseMessage> GetColorsById(IReadOnlyList<int> colorIds)
        {
            using (var request = new GetColorsByIdRequest(colorIds))
            {
                return await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            }
        }

        public async Task<HttpResponseMessage> GetColorsPage(int page, int? pageSize)
        {
            using (var request = new GetColorsPageRequest(page, pageSize))
            {
                return await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            }
        }
    }
}
