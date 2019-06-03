using System;
using System.Net.Http;
using GW2SDK.Infrastructure.Recipes;
using Xunit;

namespace GW2SDK.Tests.Features.Recipes
{
    public class GetRecipeIdsRequestTest
    {
        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Unit")]
        public void Method_ShouldBeGet()
        {
            var sut = new GetRecipeIdsRequest();

            Assert.Equal(HttpMethod.Get, sut.Method);
        }

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Unit")]
        public void RequestUri_ShouldBeV2Recipes()
        {
            var sut = new GetRecipeIdsRequest();

            var expected = new Uri("/v2/recipes", UriKind.Relative);

            Assert.Equal(expected, sut.RequestUri);
        }
    }
}
