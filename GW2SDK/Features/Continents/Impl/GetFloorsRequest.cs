using System;
using System.Net.Http;

namespace GW2SDK.Continents.Impl
{
    public sealed class GetFloorsRequest : HttpRequestMessage
    {
        public GetFloorsRequest(int continentId)
            : base(HttpMethod.Get, new Uri($"/v2/continents/{continentId}/floors?ids=all", UriKind.Relative))
        {
        }
    }
}
