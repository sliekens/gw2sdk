using System;
using System.Net;
using System.Threading.Tasks;
using GW2SDK.Features.Common;
using GW2SDK.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Features.Tokens
{
    public sealed class TokenInfoService
    {
        private readonly ITokenInfoJsonService _api;

        public TokenInfoService([NotNull] ITokenInfoJsonService api)
        {
            _api = api ?? throw new ArgumentNullException(nameof(api));
        }

        public async Task<TokenInfo> GetTokenInfo([CanBeNull] JsonSerializerSettings settings = null)
        {
            var response = await _api.GetTokenInfo().ConfigureAwait(false);
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
