using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.ItemStats.Http
{
    [PublicAPI]
    public sealed class ItemStatByIdRequest
    {
        public ItemStatByIdRequest(int itemStatId)
        {
            ItemStatId = itemStatId;
        }

        public int ItemStatId { get; }

        public static implicit operator HttpRequestMessage(ItemStatByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.ItemStatId);
            var location = new Uri($"/v2/itemstats?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
