using System;
using System.Net.Http;
using GW2SDK.Impl;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Commerce.Prices.Impl
{
    public sealed class ItemPriceByIdRequest
    {
        public ItemPriceByIdRequest(int itemId)
        {
            ItemId = itemId;
        }

        public int ItemId { get; }

        public static implicit operator HttpRequestMessage(ItemPriceByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.ItemId);
            var location = new Uri($"/v2/commerce/prices?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
