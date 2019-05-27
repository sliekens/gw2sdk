using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Features.Common;
using GW2SDK.Infrastructure;
using GW2SDK.Infrastructure.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Features.Tokens
{
    public sealed class TokenInfoService
    {
        private readonly HttpClient _http;

        public TokenInfoService([NotNull] HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<TokenInfo> GetTokenInfo([CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetTokenInfoRequest())
            {
                return await HandleRequest(request, settings).ConfigureAwait(false);
            }
        }

        public async Task<TokenInfo> GetTokenInfo([NotNull] string accessToken,
            [CanBeNull] JsonSerializerSettings settings = null)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(accessToken));
            }

            using (var request = new GetTokenInfoRequest(accessToken))
            {
                return await HandleRequest(request, settings).ConfigureAwait(false);
            }
        }

        private async Task<TokenInfo> HandleRequest(GetTokenInfoRequest request, JsonSerializerSettings settings)
        {
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    var text = JObject.Parse(json)["text"].ToString();
                    throw new UnauthorizedOperationException(text);
                }

                response.EnsureSuccessStatusCode();
                return JsonConvert.DeserializeObject<TokenInfo>(json, settings ?? Json.DefaultJsonSerializerSettings);
            }
        }
    }
}
