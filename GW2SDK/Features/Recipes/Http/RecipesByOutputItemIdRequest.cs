using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;

namespace GW2SDK.Recipes.Http
{
    [PublicAPI]
    public sealed class RecipesByOutputItemIdRequest
    {
        public RecipesByOutputItemIdRequest(int outputItemId)
        {
            OutputItemId = outputItemId;
        }

        public int OutputItemId { get; }

        public static implicit operator HttpRequestMessage(RecipesByOutputItemIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("output", r.OutputItemId);
            search.Add("ids", "all");
            var location = new Uri($"/v2/recipes/search?{search}", UriKind.Relative);
            return new HttpRequestMessage(HttpMethod.Get, location);
        }
    }
}
