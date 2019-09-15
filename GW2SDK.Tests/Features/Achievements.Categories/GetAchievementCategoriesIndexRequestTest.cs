using System;
using System.Net.Http;
using GW2SDK.Achievements.Categories.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements.Categories
{
    public class GetAchievementCategoriesIndexRequestTest
    {
        [Fact]
        [Trait("Feature",  "Achievements.Categories")]
        [Trait("Category", "Unit")]
        public void Method_is_GET()
        {
            var sut = new GetAchievementCategoriesIndexRequest();

            Assert.Equal(HttpMethod.Get, sut.Method);
        }

        [Fact]
        [Trait("Feature",  "Achievements.Categories")]
        [Trait("Category", "Unit")]
        public void RequestUri_is_v2_achievements_categories()
        {
            var sut = new GetAchievementCategoriesIndexRequest();

            var expected = new Uri("/v2/achievements/categories", UriKind.Relative);

            Assert.Equal(expected, sut.RequestUri);
        }
    }
}
