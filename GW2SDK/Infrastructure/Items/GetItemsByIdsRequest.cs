﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Extensions;

namespace GW2SDK.Infrastructure.Items
{
    public sealed class GetItemsByIdsRequest : HttpRequestMessage
    {
        private GetItemsByIdsRequest([NotNull] Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            private readonly IReadOnlyList<int> _itemIds;

            public Builder([NotNull] IReadOnlyList<int> itemIds)
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

            public GetItemsByIdsRequest GetRequest()
            {
                var ids = _itemIds.ToCsv();
                var resource = $"/v2/items?ids={ids}";
                return new GetItemsByIdsRequest(new Uri(resource, UriKind.Relative));
            }
        }
    }
}
