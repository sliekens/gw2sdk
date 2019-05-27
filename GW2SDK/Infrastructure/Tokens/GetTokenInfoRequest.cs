using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GW2SDK.Infrastructure.Tokens
{
    public sealed class GetTokenInfoRequest : HttpRequestMessage
    {
        /// <summary>
        ///     Use this constructor to get token info for a given access token.
        /// </summary>
        public GetTokenInfoRequest([NotNull] string accessToken)
            : this()
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(accessToken));
            }

            Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        /// <summary>
        ///     Use this constructor if you'll configure the access token in the <see cref="HttpClient.DefaultRequestHeaders" />.
        /// </summary>
        public GetTokenInfoRequest()
            : base(HttpMethod.Get, Resource)
        {
        }

        private static string Resource => "/v2/tokeninfo";
    }
}
