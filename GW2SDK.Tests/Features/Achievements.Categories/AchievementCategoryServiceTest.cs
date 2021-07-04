using System;
using System.Threading.Tasks;
using GW2SDK.Achievements.Categories;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements.Categories
{
    public class AchievementCategoryServiceTest
    {
        private static class AchievementCategoryFact
        {
            public static void Id_is_positive(AchievementCategory actual) => Assert.InRange(actual.Id, 1, int.MaxValue);
           
            public static void Order_is_not_negative(AchievementCategory actual) => Assert.InRange(actual.Order, 0, int.MaxValue);
        }


        [Fact]
        [Trait("Feature",  "Achievements.Categories")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_achievement_categories()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AchievementCategoryService>();

            var actual = await sut.GetAchievementCategories();

            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);
            Assert.All(actual.Values,
                achievementCategory =>
                {
                    AchievementCategoryFact.Id_is_positive(achievementCategory);
                    AchievementCategoryFact.Order_is_not_negative(achievementCategory);
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements.Categories")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_achievement_category_ids()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AchievementCategoryService>();

            var actual = await sut.GetAchievementCategoriesIndex();

            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);
        }

        [Fact]
        [Trait("Feature",  "Achievements.Categories")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_an_achievement_category_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AchievementCategoryService>();

            const int achievementCategoryId = 1;

            var actual = await sut.GetAchievementCategoryById(achievementCategoryId);

            Assert.Equal(achievementCategoryId, actual.Value.Id);
        }

        [Fact]
        [Trait("Feature",  "Achievements.Categories")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_achievement_categories_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AchievementCategoryService>();

            var ids = new[] { 1, 2, 3 };

            var actual = await sut.GetAchievementCategoriesByIds(ids);

            Assert.Collection(actual.Values, first => Assert.Equal(1, first.Id), second => Assert.Equal(2, second.Id), third => Assert.Equal(3, third.Id));
        }

        [Fact]
        [Trait("Feature",  "Achievements.Categories")]
        [Trait("Category", "Unit")]
        public async Task Achievement_category_ids_cannot_be_null()
        {
            await using var services = new Composer();
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
        public async Task Achievement_category_ids_cannot_be_empty()
        {
            await using var services = new Composer();
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
        public async Task It_can_get_achievement_categories_by_page()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AchievementCategoryService>();

            var actual = await sut.GetAchievementCategoriesByPage(1, 3);

            Assert.Equal(3, actual.Values.Count);
            Assert.Equal(3, actual.Context.PageSize);
        }
    }
}
