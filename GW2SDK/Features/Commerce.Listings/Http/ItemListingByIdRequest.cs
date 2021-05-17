using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Commerce.Listings.Http
{
    [PublicAPI]
    public sealed class ItemListingByIdRequest
    {
        public ItemListingByIdRequest(int itemId)
        {
            ItemId = itemId;
        }

        public int ItemId { get; }

        public static implicit operator HttpRequestMessage(ItemListingByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.ItemId);
            var location = new Uri($"/v2/commerce/listings?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
