using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Infrastructure.Achievements.Categories;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements.Categories
{
    public class GetAchievementCategoriesByIdsRequestBuilderTest
    {
        [Fact]
        [Trait("Feature",  "Achievements.Categories")]
        [Trait("Category", "Unit")]
        public void Constructor_WithIdsNull_ShouldThrowArgumentNullException()
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
        public void Constructor_WithIdsEmpty_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>("achievementCategoryIds",
                () =>
                {
                    _ = new GetAchievementCategoriesByIdsRequest.Builder(new int[0]);
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements.Categories")]
        [Trait("Category", "Unit")]
        public void GetRequest_MethodShouldBeGet()
        {
            var ids = new List<int> { 1, 2, 3 };

            var sut = new GetAchievementCategoriesByIdsRequest.Builder(ids);

            var actual = sut.GetRequest();

            Assert.Equal(HttpMethod.Get, actual.Method);
        }

        [Fact]
        [Trait("Feature",  "Achievements.Categories")]
        [Trait("Category", "Unit")]
        public void GetRequest_ShouldSerializeIdsAsQueryString()
        {
            var ids = new List<int> { 1, 2, 3 };

            var sut = new GetAchievementCategoriesByIdsRequest.Builder(ids);

            var actual = sut.GetRequest();

            var expected = new Uri("/v2/achievements/categories?ids=1,2,3", UriKind.Relative);

            Assert.Equal(expected, actual.RequestUri);
        }
    }
}
