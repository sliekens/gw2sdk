﻿using System;
using System.Net.Http;

namespace GW2SDK.Infrastructure.Recipes
{
    public sealed class GetRecipeIdsRequest : HttpRequestMessage
    {
        public GetRecipeIdsRequest()
            : base(HttpMethod.Get, new Uri("/v2/recipes", UriKind.Relative))
        {
        }
    }
}
