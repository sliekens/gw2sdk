using System;
using System.Net.Http;
using GW2SDK.Annotations;

namespace GW2SDK.Commerce.Prices.Impl
{
    public sealed class GetItemPriceByIdRequest : HttpRequestMessage
    {
        public GetItemPriceByIdRequest([NotNull] Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            private readonly int _itemId;

            public Builder(int itemId)
            {
                _itemId = itemId;
            }

            public GetItemPriceByIdRequest GetRequest()
            {
                var resource = new Uri($"/v2/commerce/prices?id={_itemId}", UriKind.Relative);
                return new GetItemPriceByIdRequest(resource);
            }
        }
    }
}
