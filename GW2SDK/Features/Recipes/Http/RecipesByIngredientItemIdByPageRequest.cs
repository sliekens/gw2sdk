using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;

namespace GW2SDK.Recipes.Http
{
    [PublicAPI]
    public sealed class RecipesByIngredientItemIdByPageRequest
    {
        public RecipesByIngredientItemIdByPageRequest(int ingredientItemId, int pageIndex, int? pageSize)
        {
            IngredientItemId = ingredientItemId;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
        public int IngredientItemId { get; }

        public int PageIndex { get; }

        public int? PageSize { get; }

        public static implicit operator HttpRequestMessage(RecipesByIngredientItemIdByPageRequest r)
        {
            var search = new QueryBuilder();
            search.Add("input", r.IngredientItemId);
            search.Add("page", r.PageIndex);
            if (r.PageSize.HasValue) search.Add("page_size", r.PageSize.Value);
            var location = new Uri($"/v2/recipes/search?{search}", UriKind.Relative);
            return new HttpRequestMessage(HttpMethod.Get, location);
        }
    }
}
