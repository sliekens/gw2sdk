using System.Net.Http;

namespace GW2SDK.Infrastructure.Tokens
{
    public sealed class GetTokenInfoRequest : HttpRequestMessage
    {
        public GetTokenInfoRequest()
            : base(HttpMethod.Get, Resource)
        {
        }

        private static string Resource => "/v2/tokeninfo";
    }
}