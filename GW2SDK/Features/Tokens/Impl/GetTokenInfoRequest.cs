using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GW2SDK.Tokens.Impl
{
    public sealed class GetTokenInfoRequest : HttpRequestMessage
    {
        private GetTokenInfoRequest(Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            public Builder(string? accessToken)
            {
                if (!string.IsNullOrEmpty(accessToken))
                {
                    AccessToken = new AuthenticationHeaderValue("Bearer", accessToken);
                }
            }

            public AuthenticationHeaderValue? AccessToken { get; }

            public GetTokenInfoRequest GetRequest() =>
                new GetTokenInfoRequest(new Uri("/v2/tokeninfo", UriKind.Relative)) { Headers = { Authorization = AccessToken } };
        }
    }
}
