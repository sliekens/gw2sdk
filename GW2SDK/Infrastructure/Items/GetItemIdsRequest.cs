using System;
using System.Net.Http;

namespace GW2SDK.Infrastructure.Items
{
    public sealed class GetItemIdsRequest : HttpRequestMessage
    {
        public GetItemIdsRequest()
            : base(HttpMethod.Get, new Uri("/v2/items", UriKind.Relative))
        {
        }
    }
}
