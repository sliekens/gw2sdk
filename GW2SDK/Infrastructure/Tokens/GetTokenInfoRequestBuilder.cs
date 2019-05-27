using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GW2SDK.Infrastructure.Tokens
{
    public sealed class GetTokenInfoRequestBuilder
    {
        public string Path => "/v2/tokeninfo";

        public AuthenticationHeaderValue AccessToken { get; private set; }

        public void UseAccessToken([NotNull] string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(accessToken));
            }

            AccessToken = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        public GetTokenInfoRequest Build() => new GetTokenInfoRequest(HttpMethod.Get, new Uri(Path, UriKind.Relative))
        {
            Headers =
            {
                Authorization = AccessToken
            }
        };
    }
}
