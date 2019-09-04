using System;
using System.Net.Http;

namespace GW2SDK.Continents.Impl
{
    public sealed class GetContinentsRequest : HttpRequestMessage
    {
        public GetContinentsRequest()
            : base(HttpMethod.Get, new Uri("/v2/continents?ids=all", UriKind.Relative))
        {
        }
    }
}
