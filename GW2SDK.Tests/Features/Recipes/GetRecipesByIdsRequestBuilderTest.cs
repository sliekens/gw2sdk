using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using GW2SDK.Infrastructure.Recipes;
using Xunit;

namespace GW2SDK.Tests.Features.Recipes
{
    public class GetRecipesByIdsRequestBuilderTest
    {
        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Unit")]
        public void Constructor_WithIdsNull_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>("recipeIds",
                () =>
                {
                    _ = new GetRecipesByIdsRequest.Builder(null);
                });
        }

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Unit")]
        public void Constructor_WithIdsEmpty_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>("recipeIds",
                () =>
                {
                    _ = new GetRecipesByIdsRequest.Builder(Enumerable.Empty<int>().ToList());
                });
        }

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Unit")]
        public void GetRequest_MethodShouldBeGet()
        {
            var ids = new List<int> { 1, 2, 3 };

            var sut = new GetRecipesByIdsRequest.Builder(ids);

            var actual = sut.GetRequest();

            Assert.Equal(HttpMethod.Get, actual.Method);
        }

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Unit")]
        public void GetRequest_ShouldSerializeIdsAsQueryString()
        {
            var ids = new List<int> { 1, 2, 3 };

            var sut = new GetRecipesByIdsRequest.Builder(ids);

            var actual = sut.GetRequest();

            var expected = new Uri("/v2/recipes?ids=1,2,3", UriKind.Relative);

            Assert.Equal(expected, actual.RequestUri);
        }
    }
}
