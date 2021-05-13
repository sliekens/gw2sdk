using System;
using System.Collections.Generic;
using System.Net.Http;
using JetBrains.Annotations;
using GW2SDK.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Recipes.Http
{
    [PublicAPI]
    public sealed class RecipesByIdsRequest
    {
        public RecipesByIdsRequest(IReadOnlyCollection<int> recipeIds)
        {
            if (recipeIds is null)
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
