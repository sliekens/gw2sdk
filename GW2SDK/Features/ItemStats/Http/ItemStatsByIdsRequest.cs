using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.ItemStats.Http
{
    [PublicAPI]
    public sealed class ItemStatsByIdsRequest
    {
        public ItemStatsByIdsRequest(IReadOnlyCollection<int> itemStatIds)
        {
            if (itemStatIds is null)
            {
                throw new ArgumentNullException(nameof(itemStatIds));
            }

            if (itemStatIds.Count == 0)
            {
                throw new ArgumentException("Item stat IDs cannot be an empty collection.", nameof(itemStatIds));
            }

            ItemStatIds = itemStatIds;
        }

        public IReadOnlyCollection<int> ItemStatIds { get; }

        public static implicit operator HttpRequestMessage(ItemStatsByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.ItemStatIds);
            var location = new Uri($"/v2/itemstats?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
