using System;
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

        [Fact]
        [Trait("Feature",  "Achievements.Categories")]
        [Trait("Category", "Integration")]
        public async Task GetAchievementCategoriesIndex_ShouldReturnAllIds()
        {
            var services = new Container();
            var sut = services.Resolve<AchievementCategoryService>();

            var actual = await sut.GetAchievementCategoriesIndex();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Achievements.Categories")]
        [Trait("Category", "Integration")]
        public async Task GetAchievementCategoryById_ShouldReturnThatAchievementCategory()
        {
            var services = new Container();
            var sut = services.Resolve<AchievementCategoryService>();

            const int achievementCategoryId = 1;

            var actual = await sut.GetAchievementCategoryById(achievementCategoryId);

            Assert.Equal(achievementCategoryId, actual.Id);
        }

        [Fact]
        [Trait("Feature",  "Achievements.Categories")]
        [Trait("Category", "Unit")]
        public async Task GetAchievementCategoriesByIds_WithIdsNull_ShouldThrowArgumentNullException()
        {
            var services = new Container();
            var sut = services.Resolve<AchievementCategoryService>();

            await Assert.ThrowsAsync<ArgumentNullException>("achievementCategoryIds",
                async () =>
                {
                    await sut.GetAchievementCategoriesByIds(null);
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements.Categories")]
        [Trait("Category", "Unit")]
        public async Task GetAchievementCategoriesByIds_WithIdsEmpty_ShouldThrowArgumentException()
        {
            var services = new Container();
            var sut = services.Resolve<AchievementCategoryService>();

            await Assert.ThrowsAsync<ArgumentException>("achievementCategoryIds",
                async () =>
                {
                    await sut.GetAchievementCategoriesByIds(new int[0]);
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements.Categories")]
        [Trait("Category", "Integration")]
        public async Task GetAchievementCategoriesByIds_ShouldReturnThoseAchievementCategories()
        {
            var services = new Container();
            var sut = services.Resolve<AchievementCategoryService>();

            var ids = new[] { 1, 2, 3 };

            var actual = await sut.GetAchievementCategoriesByIds(ids);

            Assert.Collection(actual, first => Assert.Equal(1, first.Id), second => Assert.Equal(2, second.Id), third => Assert.Equal(3, third.Id));
        }

        [Fact]
        [Trait("Feature",  "Achievements.Categories")]
        [Trait("Category", "Integration")]
        public async Task GetAchievementCategoriesByPage_WithInvalidPage_ShouldThrowArgumentException()
        {
            var services = new Container();
            var sut = services.Resolve<AchievementCategoryService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetAchievementCategoriesByPage(-1, 3));
        }

        [Fact]
        [Trait("Feature",  "Achievements.Categories")]
        [Trait("Category", "Integration")]
        public async Task GetAchievementCategoriesByPage_WithInvalidPageSize_ShouldThrowArgumentException()
        {
            var services = new Container();
            var sut = services.Resolve<AchievementCategoryService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetAchievementCategoriesByPage(1, -3));
        }

        [Fact]
        [Trait("Feature",  "Achievements.Categories")]
        [Trait("Category", "Integration")]
        public async Task GetAchievementCategoriesByPage_WithPage1AndPageSize3_ShouldReturnThatPage()
        {
            var services = new Container();
            var sut = services.Resolve<AchievementCategoryService>();

            var actual = await sut.GetAchievementCategoriesByPage(1, 3);

            Assert.Equal(3, actual.Count);
            Assert.Equal(3, actual.PageSize);
        }
    }
}
