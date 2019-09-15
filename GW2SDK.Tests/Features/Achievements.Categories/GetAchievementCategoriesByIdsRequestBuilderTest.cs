using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Achievements.Categories.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements.Categories
{
    public class GetAchievementCategoriesByIdsRequestBuilderTest
    {
        [Fact]
        [Trait("Feature",  "Achievements.Categories")]
        [Trait("Category", "Unit")]
        public void Method_is_GET()
        {
            var ids = new List<int> { 1, 2, 3 };

            var sut = new GetAchievementCategoriesByIdsRequest.Builder(ids);

            var actual = sut.GetRequest();

            Assert.Equal(HttpMethod.Get, actual.Method);
        }

        [Fact]
        [Trait("Feature",  "Achievements.Categories")]
        [Trait("Category", "Unit")]
        public void RequestUri_contains_specified_ids()
        {
            var ids = new List<int> { 1, 2, 3 };

            var sut = new GetAchievementCategoriesByIdsRequest.Builder(ids);

            var actual = sut.GetRequest();

            var expected = new Uri("/v2/achievements/categories?ids=1,2,3", UriKind.Relative);

            Assert.Equal(expected, actual.RequestUri);
        }

        [Fact]
        [Trait("Feature",  "Achievements.Categories")]
        [Trait("Category", "Unit")]
        public void Achievement_category_ids_cannot_be_null()
        {
            Assert.Throws<ArgumentNullException>("achievementCategoryIds",
                () =>
                {
                    _ = new GetAchievementCategoriesByIdsRequest.Builder(null);
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements.Categories")]
        [Trait("Category", "Unit")]
        public void Achievement_category_ids_cannot_be_empty()
        {
            Assert.Throws<ArgumentException>("achievementCategoryIds",
                () =>
                {
                    _ = new GetAchievementCategoriesByIdsRequest.Builder(new int[0]);
                });
        }
    }
}
