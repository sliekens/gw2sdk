using System;
using System.Net.Http;

namespace GW2SDK.Infrastructure.Subtokens
{
    public sealed class CreateSubtokenRequest : HttpRequestMessage
    {
        public CreateSubtokenRequest(HttpMethod method, Uri requestUri) : base(method, requestUri)
        {
        }
    }
}
