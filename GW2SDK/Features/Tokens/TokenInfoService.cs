using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Tokens.Http;
using JetBrains.Annotations;

namespace GW2SDK.Tokens
{
    [PublicAPI]
    public sealed class TokenInfoService
    {
        private readonly HttpClient _http;
        private readonly MissingMemberBehavior _missingMemberBehavior;

        private readonly ITokenInfoReader _tokenInfoReader;

        public TokenInfoService(
            HttpClient http,
            ITokenInfoReader tokenInfoReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _tokenInfoReader = tokenInfoReader ?? throw new ArgumentNullException(nameof(tokenInfoReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplica<TokenInfo>> GetTokenInfo(string? accessToken)
        {
            var request = new TokenInfoRequest(accessToken);
            return await _http.GetResource(request, json => _tokenInfoReader.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
