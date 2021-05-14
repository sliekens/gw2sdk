using System;
using System.Net.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Recipes.Http
{
    [PublicAPI]
    public sealed class RecipesIndexByIngredientItemIdRequest
    {
        public RecipesIndexByIngredientItemIdRequest(int ingredientItemId)
        {
            IngredientItemId = ingredientItemId;
        }

        public int IngredientItemId { get; }

        public static implicit operator HttpRequestMessage(RecipesIndexByIngredientItemIdRequest r)
        {
            var location = new Uri($"/v2/recipes/search?input={r.IngredientItemId}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
