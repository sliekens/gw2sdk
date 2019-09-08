using System;
using System.Net.Http;
using GW2SDK.Annotations;

namespace GW2SDK.Recipes.Impl
{
    public sealed class GetRecipesByPageRequest : HttpRequestMessage
    {
        private GetRecipesByPageRequest([NotNull] Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            private readonly int _pageIndex;

            private readonly int? _pageSize;

            public Builder(int pageIndex, int? pageSize = null)
            {
                _pageIndex = pageIndex;
                _pageSize = pageSize;
            }

            public GetRecipesByPageRequest GetRequest()
            {
                var resource = $"/v2/recipes?page={_pageIndex}";
                if (_pageSize.HasValue)
                {
                    resource += $"&page_size={_pageSize.Value}";
                }

                return new GetRecipesByPageRequest(new Uri(resource, UriKind.Relative));
            }
        }
    }
}
