using System;
using System.Net.Http;
using System.Net.Http.Headers;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Tokens.Impl
{
    public sealed class TokenInfoRequest
    {
        public TokenInfoRequest(string? accessToken)
        {
            if (!string.IsNullOrEmpty(accessToken))
            {
                AccessToken = new AuthenticationHeaderValue("Bearer", accessToken);
            }
        }

        public AuthenticationHeaderValue? AccessToken { get; }

        public static implicit operator HttpRequestMessage(TokenInfoRequest r)
        {
            var location = new Uri("/v2/tokeninfo", UriKind.Relative);
            return new HttpRequestMessage(Get, location) { Headers = { Authorization = r.AccessToken } };
        }
    }
}
