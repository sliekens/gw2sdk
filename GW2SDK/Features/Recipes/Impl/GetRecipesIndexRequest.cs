using System;
using System.Net.Http;

namespace GW2SDK.Recipes.Impl
{
    public sealed class GetRecipesIndexRequest : HttpRequestMessage
    {
        public GetRecipesIndexRequest()
            : base(HttpMethod.Get, new Uri("/v2/recipes", UriKind.Relative))
        {
        }
    }
}
