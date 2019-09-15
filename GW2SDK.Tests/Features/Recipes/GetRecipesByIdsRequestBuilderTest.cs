using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Recipes.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Recipes
{
    public class GetRecipesByIdsRequestBuilderTest
    {
        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Unit")]
        public void Method_is_GET()
        {
            var ids = new List<int> { 1, 2, 3 };

            var sut = new GetRecipesByIdsRequest.Builder(ids);

            var actual = sut.GetRequest();

            Assert.Equal(HttpMethod.Get, actual.Method);
        }

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Unit")]
        public void RequestUri_contains_specified_ids()
        {
            var ids = new List<int> { 1, 2, 3 };

            var sut = new GetRecipesByIdsRequest.Builder(ids);

            var actual = sut.GetRequest();

            var expected = new Uri("/v2/recipes?ids=1,2,3", UriKind.Relative);

            Assert.Equal(expected, actual.RequestUri);
        }

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Unit")]
        public void Recipe_ids_cannot_be_null()
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
        public void Recipe_ids_cannot_be_empty()
        {
            Assert.Throws<ArgumentException>("recipeIds",
                () =>
                {
                    _ = new GetRecipesByIdsRequest.Builder(new int[0]);
                });
        }
    }
}
