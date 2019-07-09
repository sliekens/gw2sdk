using System;
using System.Net.Http;
using GW2SDK.Recipes.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Recipes
{
    public class GetRecipesByPageRequestBuilderTest
    {
        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Unit")]
        public void GetRequest_MethodShouldBeGet()
        {
            var sut = new GetRecipesByPageRequest.Builder(0);

            var actual = sut.GetRequest();

            Assert.Equal(HttpMethod.Get, actual.Method);
        }

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Unit")]
        public void GetRequest_ShouldSerializePageAsQueryString()
        {
            var sut = new GetRecipesByPageRequest.Builder(1);

            var actual = sut.GetRequest();

            var expected = new Uri("/v2/recipes?page=1", UriKind.Relative);

            Assert.Equal(expected, actual.RequestUri);
        }

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Unit")]
        public void GetRequest_WithPageSize_ShouldSerializePageSizeAsQueryString()
        {
            var sut = new GetRecipesByPageRequest.Builder(1, 200);

            var actual = sut.GetRequest();

            var expected = new Uri("/v2/recipes?page=1&page_size=200", UriKind.Relative);

            Assert.Equal(expected, actual.RequestUri);
        }
    }
}
