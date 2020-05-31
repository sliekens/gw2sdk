using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Impl;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Recipes.Impl
{
    public sealed class RecipesByIdsRequest
    {
        public RecipesByIdsRequest(IReadOnlyCollection<int> recipeIds)
        {
            if (recipeIds == null)
            {
                throw new ArgumentNullException(nameof(recipeIds));
            }

            if (recipeIds.Count == 0)
            {
                throw new ArgumentException("Recipe IDs cannot be an empty collection.", nameof(recipeIds));
            }

            RecipeIds = recipeIds;
        }

        public IReadOnlyCollection<int> RecipeIds { get; }

        public static implicit operator HttpRequestMessage(RecipesByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.RecipeIds);
            var location = new Uri($"/v2/recipes?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
