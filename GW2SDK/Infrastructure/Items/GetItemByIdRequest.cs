﻿using System;
using System.Net.Http;

namespace GW2SDK.Infrastructure.Items
{
    public sealed class GetItemByIdRequest : HttpRequestMessage
    {
        public GetItemByIdRequest([NotNull] Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            private readonly int _itemId;

            public Builder(int itemId)
            {
                _itemId = itemId;
            }

            public GetItemByIdRequest GetRequest()
            {
                var resource = new Uri($"/v2/items?id={_itemId}", UriKind.Relative);
                return new GetItemByIdRequest(resource);
            }
        }
    }
}
