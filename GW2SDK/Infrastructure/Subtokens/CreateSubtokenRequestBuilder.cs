using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GW2SDK.Infrastructure.Subtokens
{
    public sealed class CreateSubtokenRequestBuilder
    {
        public string Path => "/v2/createsubtoken";

        public AuthenticationHeaderValue AccessToken { get; private set; }

        public void UseAccessToken([NotNull] string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(accessToken));
            }

            AccessToken = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        public CreateSubtokenRequest Build() => new CreateSubtokenRequest(HttpMethod.Get, new Uri(Path, UriKind.Relative))
        {
            Headers =
            {
                Authorization = AccessToken
            }
        };
    }
}