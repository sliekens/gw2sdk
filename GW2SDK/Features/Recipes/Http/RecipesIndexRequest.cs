using System;
using System.Net.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Recipes.Http
{
    [PublicAPI]
    public sealed class RecipesIndexRequest
    {
        public static implicit operator HttpRequestMessage(RecipesIndexRequest _)
        {
            var location = new Uri("/v2/recipes", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
