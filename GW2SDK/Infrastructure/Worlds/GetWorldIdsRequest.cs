using System;
using System.Net.Http;

namespace GW2SDK.Infrastructure.Worlds
{
    public sealed class GetWorldIdsRequest : HttpRequestMessage
    {
        public GetWorldIdsRequest()
            : base(HttpMethod.Get, new Uri("/v2/worlds", UriKind.Relative))
        {
        }
    }
}
