using System;
using System.Net.Http;
using GW2SDK.Achievements.Categories.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements.Categories
{
    public class GetAchievementCategoriesRequestTest
    {
        [Fact]
        [Trait("Feature",  "Achievements.Categories")]
        [Trait("Category", "Unit")]
        public void Method_is_GET()
        {
            var sut = new GetAchievementCategoriesRequest();

            Assert.Equal(HttpMethod.Get, sut.Method);
        }

        [Fact]
        [Trait("Feature",  "Achievements.Categories")]
        [Trait("Category", "Unit")]
        public void RequestUri_is_v2_achievements_categories_bulk()
        {
            var sut = new GetAchievementCategoriesRequest();

            var expected = new Uri("/v2/achievements/categories?ids=all", UriKind.Relative);

            Assert.Equal(expected, sut.RequestUri);
        }
    }
}
