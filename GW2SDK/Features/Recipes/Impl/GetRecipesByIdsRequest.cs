using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Extensions;

namespace GW2SDK.Recipes.Impl
{
    public sealed class GetRecipesByIdsRequest : HttpRequestMessage
    {
        private GetRecipesByIdsRequest(Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            private readonly IReadOnlyList<int> _recipeIds;

            public Builder(IReadOnlyList<int> recipeIds)
            {
                if (recipeIds == null)
                {
                    throw new ArgumentNullException(nameof(recipeIds));
                }

                if (recipeIds.Count == 0)
                {
                    throw new ArgumentException("Recipe IDs cannot be an empty collection.", nameof(recipeIds));
                }

                _recipeIds = recipeIds;
            }

            public GetRecipesByIdsRequest GetRequest()
            {
                var ids = _recipeIds.ToCsv();
                var resource = $"/v2/recipes?ids={ids}";
                return new GetRecipesByIdsRequest(new Uri(resource, UriKind.Relative));
            }
        }
    }
}
