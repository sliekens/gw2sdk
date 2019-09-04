using System;
using System.Net.Http;

namespace GW2SDK.Continents.Impl
{
    public sealed class GetContinentsIndexRequest : HttpRequestMessage
    {
        public GetContinentsIndexRequest()
            : base(HttpMethod.Get, new Uri("/v2/continents", UriKind.Relative))
        {
        }
    }
}
