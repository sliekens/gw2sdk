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
        private readonly HttpClient http;
        private readonly MissingMemberBehavior missingMemberBehavior;

        private readonly ITokenInfoReader tokenInfoReader;

        public TokenInfoService(
            HttpClient http,
            ITokenInfoReader tokenInfoReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
            this.tokenInfoReader = tokenInfoReader ?? throw new ArgumentNullException(nameof(tokenInfoReader));
            this.missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplica<TokenInfo>> GetTokenInfo(string? accessToken)
        {
            var request = new TokenInfoRequest(accessToken);
            return await http.GetResource(request, json => tokenInfoReader.Read(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
