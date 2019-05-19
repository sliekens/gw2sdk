using System;
using System.Threading.Tasks;
using GW2SDK.Features.Tokens.Infrastructure;
using GW2SDK.Infrastructure;
using Newtonsoft.Json;

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
            var (json, _) = await _api.GetTokenInfo();
            return JsonConvert.DeserializeObject<TokenInfo>(json, settings ?? Json.DefaultJsonSerializerSettings);
        }
    }
}
