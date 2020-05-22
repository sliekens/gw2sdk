using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Tokens.Impl;
using Newtonsoft.Json;

namespace GW2SDK.Tokens
{
    [PublicAPI]
    public sealed class TokenInfoService
    {
        private readonly HttpClient _http;

        public TokenInfoService(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<TokenInfo?> GetTokenInfo(string? accessToken, JsonSerializerSettings? settings = null)
        {
            using var request = new GetTokenInfoRequest.Builder(accessToken).GetRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<TokenInfo>(json, settings ?? Json.DefaultJsonSerializerSettings);
        }
    }
}
