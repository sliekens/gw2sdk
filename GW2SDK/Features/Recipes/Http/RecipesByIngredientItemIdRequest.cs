using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;

namespace GW2SDK.Recipes.Http
{
    [PublicAPI]
    public sealed class RecipesByIngredientItemIdRequest
    {
        public RecipesByIngredientItemIdRequest(int ingredientItemId)
        {
            IngredientItemId = ingredientItemId;
        }

        public int IngredientItemId { get; }

        public static implicit operator HttpRequestMessage(RecipesByIngredientItemIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("input", r.IngredientItemId);
            search.Add("ids", "all");
            var location = new Uri($"/v2/recipes/search?{search}", UriKind.Relative);
            return new HttpRequestMessage(HttpMethod.Get, location);
        }
    }
}
