using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Tokens.Http;
using GW2SDK.Tokens.Json;
using JetBrains.Annotations;

namespace GW2SDK.Tokens
{
    [PublicAPI]
    public sealed class TokenInfoService
    {
        private readonly HttpClient http;

        public TokenInfoService(HttpClient http)
        {
            this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IReplica<TokenInfo>> GetTokenInfo(
            string? accessToken,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new TokenInfoRequest(accessToken);
            return await http.GetResource(request,
                    json => TokenInfoReader.Read(json.RootElement, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
