using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Recipes.Http
{
    [PublicAPI]
    public sealed class RecipeByIdRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/recipes")
        {
            AcceptEncoding = "gzip"
        };

        public RecipeByIdRequest(int recipeId)
        {
            RecipeId = recipeId;
        }

        public int RecipeId { get; }

        public static implicit operator HttpRequestMessage(RecipeByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.RecipeId);
            var request = Template with
            {
                Arguments = search
            };
            return request.Compile();
        }
    }
}
