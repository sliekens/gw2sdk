using System;
using System.Net.Http;
using GW2SDK.Impl;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Items.Impl
{
    public sealed class ItemByIdRequest
    {
        public ItemByIdRequest(int itemId)
        {
            ItemId = itemId;
        }

        public int ItemId { get; }

        public static implicit operator HttpRequestMessage(ItemByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.ItemId);
            var location = new Uri($"/v2/items?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
