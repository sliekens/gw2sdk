using System;
using System.Net.Http;
using JetBrains.Annotations;
using GW2SDK.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Recipes.Http
{
    [PublicAPI]
    public sealed class RecipeByIdRequest
    {
        public RecipeByIdRequest(int recipeId)
        {
            RecipeId = recipeId;
        }

        public int RecipeId { get; }

        public static implicit operator HttpRequestMessage(RecipeByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.RecipeId);
            var location = new Uri($"/v2/recipes?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
