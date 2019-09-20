using System;
using System.Net.Http;

namespace GW2SDK.Commerce.Prices.Impl
{
    public sealed class GetItemPricesIndexRequest : HttpRequestMessage
    {
        public GetItemPricesIndexRequest()
            : base(HttpMethod.Get, new Uri("/v2/commerce/prices", UriKind.Relative))
        {
        }
    }
}
