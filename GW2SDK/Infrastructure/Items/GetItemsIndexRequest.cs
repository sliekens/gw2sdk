using System;
using System.Net.Http;

namespace GW2SDK.Infrastructure.Items
{
    public sealed class GetItemsIndexRequest : HttpRequestMessage
    {
        public GetItemsIndexRequest()
            : base(HttpMethod.Get, new Uri("/v2/items", UriKind.Relative))
        {
        }
    }
}
