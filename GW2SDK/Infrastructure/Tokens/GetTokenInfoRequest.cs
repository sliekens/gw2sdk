using System;
using System.Net.Http;

namespace GW2SDK.Infrastructure.Tokens
{
    public sealed class GetTokenInfoRequest : HttpRequestMessage
    {
        public GetTokenInfoRequest([NotNull] HttpMethod method, [NotNull] Uri requestUri)
            : base(method, requestUri)
        {
        }
    }
}
