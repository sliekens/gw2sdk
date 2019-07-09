using System;
using System.Net.Http;

namespace GW2SDK.Worlds.Impl
{
    public sealed class GetWorldsRequest : HttpRequestMessage
    {
        public GetWorldsRequest()
            : base(HttpMethod.Get, new Uri("/v2/worlds?ids=all", UriKind.Relative))
        {
        }
    }
}
