using System;
using System.Net.Http;

namespace GW2SDK.Continents.Impl
{
    public sealed class GetFloorsIndexRequest : HttpRequestMessage
    {
        public GetFloorsIndexRequest(int continentId)
            : base(HttpMethod.Get, new Uri($"/v2/continents/{continentId}/floors", UriKind.Relative))
        {
        }
    }
}
