using System;
using System.Net.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Commerce.Prices.Impl
{
    public sealed class ItemPricesIndexRequest
    {
        public static implicit operator HttpRequestMessage(ItemPricesIndexRequest _)
        {
            var location = new Uri("/v2/commerce/prices", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
