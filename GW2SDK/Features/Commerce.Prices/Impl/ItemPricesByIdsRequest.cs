using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Impl;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Commerce.Prices.Impl
{
    public sealed class ItemPricesByIdsRequest
    {
        public ItemPricesByIdsRequest(IReadOnlyCollection<int> itemIds)
        {
            if (itemIds == null)
            {
                throw new ArgumentNullException(nameof(itemIds));
            }

            if (itemIds.Count == 0)
            {
                throw new ArgumentException("Item IDs cannot be an empty collection.", nameof(itemIds));
            }

            ItemIds = itemIds;
        }

        public IReadOnlyCollection<int> ItemIds { get; }

        public static implicit operator HttpRequestMessage(ItemPricesByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.ItemIds);
            var location = new Uri($"/v2/commerce/prices?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
