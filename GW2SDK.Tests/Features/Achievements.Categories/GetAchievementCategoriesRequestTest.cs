using System;
using System.Net.Http;
using GW2SDK.Infrastructure.Achievements.Categories;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements.Categories
{
    public class GetAchievementCategoriesRequestTest
    {
        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Unit")]
        public void Method_ShouldBeGet()
        {
            var sut = new GetAchievementCategoriesRequest();

            Assert.Equal(HttpMethod.Get, sut.Method);
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Unit")]
        public void RequestUri_ShouldBeV2AchievementsCategoriesBulk()
        {
            var sut = new GetAchievementCategoriesRequest();

            var expected = new Uri("/v2/achievements/categories?ids=all", UriKind.Relative);

            Assert.Equal(expected, sut.RequestUri);
        }
    }
}
