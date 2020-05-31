using System;
using System.Net.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Recipes.Search.Impl
{
    public sealed class RecipesIndexByItemIdRequest
    {
        public RecipesIndexByItemIdRequest(int itemId)
        {
            ItemId = itemId;
        }

        public int ItemId { get; }

        public static implicit operator HttpRequestMessage(RecipesIndexByItemIdRequest r)
        {
            var location = new Uri($"/v2/recipes/search?output={r.ItemId}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
