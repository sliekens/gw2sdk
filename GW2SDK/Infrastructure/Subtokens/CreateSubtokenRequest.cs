using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GW2SDK.Infrastructure.Subtokens
{
    public sealed class CreateSubtokenRequest : HttpRequestMessage
    {
        private CreateSubtokenRequest([NotNull] Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            public Builder([CanBeNull] string accessToken = null)
            {
                if (!string.IsNullOrEmpty(accessToken))
                {
                    AccessToken = new AuthenticationHeaderValue("Bearer", accessToken);
                }
            }

            public AuthenticationHeaderValue AccessToken { get; }

            public CreateSubtokenRequest GetRequest() =>
                new CreateSubtokenRequest(new Uri("/v2/createsubtoken", UriKind.Relative)) { Headers = { Authorization = AccessToken } };
        }
    }
}
