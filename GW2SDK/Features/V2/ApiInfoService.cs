using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Http;
using GW2SDK.V2.Http;

namespace GW2SDK.V2
{
    [PublicAPI]
    public sealed class ApiInfoService
    {
        private readonly IApiInfoReader _apiInfoReader;
        private readonly HttpClient _http;

        public ApiInfoService(HttpClient http, IApiInfoReader apiInfoReader)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _apiInfoReader = apiInfoReader ?? throw new ArgumentNullException(nameof(apiInfoReader));
        }

        public async Task<ApiInfo?> GetApiInfo()
        {
            var request = new ApiInfoRequest();
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            return _apiInfoReader.Read(json);
        }
    }
}
