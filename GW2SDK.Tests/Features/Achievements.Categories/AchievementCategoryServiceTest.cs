using System.Threading.Tasks;
using GW2SDK.Features.Achievements.Categories;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements.Categories
{
    public class AchievementCategoryServiceTest
    {
        [Fact]
        [Trait("Feature",  "Achievements.Categories")]
        [Trait("Category", "Integration")]
        public async Task GetAchievementCategories_ShouldReturnAllAchievementCategories()
        {
            var services = new Container();
            var sut = services.Resolve<AchievementCategoryService>();

            var actual = await sut.GetAchievementCategories();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }
    }
}
