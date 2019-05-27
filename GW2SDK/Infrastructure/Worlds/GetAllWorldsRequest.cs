using System;
using System.Net.Http;

namespace GW2SDK.Infrastructure.Worlds
{
    public sealed class GetAllWorldsRequest : HttpRequestMessage
    {
        public GetAllWorldsRequest()
            : base(HttpMethod.Get, new Uri("/v2/worlds?ids=all", UriKind.Relative))
        {
        }
    }
}
