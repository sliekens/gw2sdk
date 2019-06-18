using System;
using System.Net.Http;
using GW2SDK.Infrastructure.Achievements.Categories;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements.Categories
{
    public class GetAchievementCategoriesIndexRequestTest
    {
        [Fact]
        [Trait("Feature",  "Achievements.Categories")]
        [Trait("Category", "Unit")]
        public void Method_ShouldBeGet()
        {
            var sut = new GetAchievementCategoriesIndexRequest();

            Assert.Equal(HttpMethod.Get, sut.Method);
        }

        [Fact]
        [Trait("Feature",  "Achievements.Categories")]
        [Trait("Category", "Unit")]
        public void RequestUri_ShouldBeV2AchievementsCategories()
        {
            var sut = new GetAchievementCategoriesIndexRequest();

            var expected = new Uri("/v2/achievements/categories", UriKind.Relative);

            Assert.Equal(expected, sut.RequestUri);
        }
    }
}
