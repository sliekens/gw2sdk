using System;
using System.Net.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Recipes.Http
{
    [PublicAPI]
    public sealed class RecipesIndexByOutputItemIdRequest
    {
        public RecipesIndexByOutputItemIdRequest(int outputItemId)
        {
            OutputItemId = outputItemId;
        }

        public int OutputItemId { get; }

        public static implicit operator HttpRequestMessage(RecipesIndexByOutputItemIdRequest r)
        {
            var location = new Uri($"/v2/recipes/search?output={r.OutputItemId}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
