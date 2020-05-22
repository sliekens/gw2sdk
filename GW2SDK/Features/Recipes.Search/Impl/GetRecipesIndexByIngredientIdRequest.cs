﻿using System;
using System.Net.Http;

namespace GW2SDK.Recipes.Search.Impl
{
    public sealed class GetRecipesIndexByIngredientIdRequest : HttpRequestMessage
    {
        private GetRecipesIndexByIngredientIdRequest(Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            private readonly int _ingredientId;

            public Builder(int ingredientId)
            {
                _ingredientId = ingredientId;
            }

            public GetRecipesIndexByIngredientIdRequest GetRequest()
            {
                var resource = new Uri($"/v2/recipes/search?input={_ingredientId}", UriKind.Relative);
                return new GetRecipesIndexByIngredientIdRequest(resource);
            }
        }
    }
}
