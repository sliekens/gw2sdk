using System;
using System.Net.Http;
using GW2SDK.Recipes.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Recipes
{
    public class GetRecipesIndexRequestTest
    {
        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Unit")]
        public void Method_is_GET()
        {
            var sut = new GetRecipesIndexRequest();

            Assert.Equal(HttpMethod.Get, sut.Method);
        }

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Unit")]
        public void RequestUri_is_v2_recipes()
        {
            var sut = new GetRecipesIndexRequest();

            var expected = new Uri("/v2/recipes", UriKind.Relative);

            Assert.Equal(expected, sut.RequestUri);
        }
    }
}
