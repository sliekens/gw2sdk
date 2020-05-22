using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Extensions;

namespace GW2SDK.Commerce.Prices.Impl
{
    public sealed class GetItemPricesByIdsRequest : HttpRequestMessage
    {
        private GetItemPricesByIdsRequest(Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            private readonly IReadOnlyList<int> _itemIds;

            public Builder(IReadOnlyList<int> itemIds)
            {
                if (itemIds == null)
                {
                    throw new ArgumentNullException(nameof(itemIds));
                }

                if (itemIds.Count == 0)
                {
                    throw new ArgumentException("Item IDs cannot be an empty collection.", nameof(itemIds));
                }

                _itemIds = itemIds;
            }

            public GetItemPricesByIdsRequest GetRequest()
            {
                var ids = _itemIds.ToCsv();
                var resource = $"/v2/commerce/prices?ids={ids}";
                return new GetItemPricesByIdsRequest(new Uri(resource, UriKind.Relative));
            }
        }
    }
}
