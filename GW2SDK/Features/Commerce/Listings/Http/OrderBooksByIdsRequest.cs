﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Commerce.Listings.Http
{
    [PublicAPI]
    public sealed class OrderBooksByIdsRequest
    {
        public OrderBooksByIdsRequest(IReadOnlyCollection<int> itemIds)
        {
            Check.Collection(itemIds, nameof(itemIds));
            ItemIds = itemIds;
        }

        public IReadOnlyCollection<int> ItemIds { get; }

        public static implicit operator HttpRequestMessage(OrderBooksByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.ItemIds);
            var location = new Uri($"/v2/commerce/listings?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
