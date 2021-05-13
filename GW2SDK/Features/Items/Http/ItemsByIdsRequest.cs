using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Annotations;
using GW2SDK.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Items.Http
{
    [PublicAPI]
    public sealed class ItemsByIdsRequest
    {
        public ItemsByIdsRequest(IReadOnlyCollection<int> itemIds)
        {
            if (itemIds is null)
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

        public static implicit operator HttpRequestMessage(ItemsByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.ItemIds);
            var location = new Uri($"/v2/items?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
