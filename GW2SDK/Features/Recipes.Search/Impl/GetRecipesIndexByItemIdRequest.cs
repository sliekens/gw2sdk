using System;
using System.Net.Http;
using GW2SDK.Annotations;

namespace GW2SDK.Recipes.Search.Impl
{
    public sealed class GetRecipesIndexByItemIdRequest : HttpRequestMessage
    {
        private GetRecipesIndexByItemIdRequest([NotNull] Uri requestUri)
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

            public GetRecipesIndexByItemIdRequest GetRequest()
            {
                var resource = new Uri($"/v2/recipes/search?output={_itemId}", UriKind.Relative);
                return new GetRecipesIndexByItemIdRequest(resource);
            }
        }
    }
}
