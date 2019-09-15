using System;
using System.Net.Http;
using GW2SDK.Recipes.Search.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Recipes.Search
{
    public class GetRecipesIndexByItemIdRequestBuilderTest
    {
        [Fact]
        [Trait("Feature",  "Recipes.Search")]
        [Trait("Category", "Unit")]
        public void Method_is_GET()
        {
            var id = 1;

            var sut = new GetRecipesIndexByItemIdRequest.Builder(id);

            var actual = sut.GetRequest();

            Assert.Equal(HttpMethod.Get, actual.Method);
        }

        [Fact]
        [Trait("Feature",  "Recipes.Search")]
        [Trait("Category", "Unit")]
        public void RequestUri_contains_specified_id()
        {
            var id = 1;

            var sut = new GetRecipesIndexByItemIdRequest.Builder(id);

            var actual = sut.GetRequest();

            var expected = new Uri("/v2/recipes/search?output=1", UriKind.Relative);

            Assert.Equal(expected, actual.RequestUri);
        }
    }
}
