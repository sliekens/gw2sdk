using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Impl.V2.Impl;
using Newtonsoft.Json;

namespace GW2SDK.Impl.V2
{
    internal sealed class ApiInfoService
    {
        private readonly HttpClient _http;

        public ApiInfoService(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<ApiInfo?> GetApiInfo()
        {
            var request = new ApiInfoRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ApiInfo>(json, JsonConverters.Json.DefaultJsonSerializerSettings);
        }
    }
}
