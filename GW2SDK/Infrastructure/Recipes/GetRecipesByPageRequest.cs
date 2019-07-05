﻿using System;
using System.Net.Http;

namespace GW2SDK.Infrastructure.Recipes
{
    public sealed class GetRecipesByPageRequest : HttpRequestMessage
    {
        private GetRecipesByPageRequest([NotNull] Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            private readonly int _page;

            private readonly int? _pageSize;

            public Builder(int page, int? pageSize = null)
            {
                _page = page;
                _pageSize = pageSize;
            }

            public GetRecipesByPageRequest GetRequest()
            {
                var resource = $"/v2/recipes?page={_page}";
                if (_pageSize.HasValue)
                {
                    resource += $"&page_size={_pageSize.Value}";
                }

                return new GetRecipesByPageRequest(new Uri(resource, UriKind.Relative));
            }
        }
    }
}
