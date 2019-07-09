using System;
using System.Net.Http;

namespace GW2SDK.Worlds.Impl
{
    public sealed class GetWorldsIndexRequest : HttpRequestMessage
    {
        public GetWorldsIndexRequest()
            : base(HttpMethod.Get, new Uri("/v2/worlds", UriKind.Relative))
        {
        }
    }
}
