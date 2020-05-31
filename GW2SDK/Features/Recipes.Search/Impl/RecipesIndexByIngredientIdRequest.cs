using System;
using System.Net.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Recipes.Search.Impl
{
    public sealed class RecipesIndexByIngredientIdRequest
    {
        public RecipesIndexByIngredientIdRequest(int ingredientId)
        {
            IngredientId = ingredientId;
        }

        public int IngredientId { get; }

        public static implicit operator HttpRequestMessage(RecipesIndexByIngredientIdRequest r)
        {
            var location = new Uri($"/v2/recipes/search?input={r.IngredientId}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
