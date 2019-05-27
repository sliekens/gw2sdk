using System;
using System.Net.Http;

namespace GW2SDK.Infrastructure.Tokens
{
    public sealed class GetTokenInfoRequest : HttpRequestMessage
    {
        public GetTokenInfoRequest(HttpMethod method, Uri requestUri) : base(method, requestUri)
        {
        }
    }
}
