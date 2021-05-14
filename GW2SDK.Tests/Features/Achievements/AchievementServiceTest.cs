using System;
using System.Threading.Tasks;
using GW2SDK.Achievements;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements
{
    public class AchievementServiceTest
    {
        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_achievement_ids()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AchievementService>();

            var actual = await sut.GetAchievementsIndex();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_an_achievement_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AchievementService>();

            const int achievementId = 1;

            var actual = await sut.GetAchievementById(achievementId);

            Assert.Equal(achievementId, actual.Id);
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_achievements_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AchievementService>();

            var achievementIds = new[] { 1, 2, 3 };

            var actual = await sut.GetAchievementsByIds(achievementIds);

            Assert.Collection(actual,
                achievement => Assert.Equal(1, achievement.Id),
                achievement => Assert.Equal(2, achievement.Id),
                achievement => Assert.Equal(3, achievement.Id));
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Unit")]
        public async Task Achievement_ids_cannot_be_null()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AchievementService>();

            await Assert.ThrowsAsync<ArgumentNullException>("achievementIds",
                async () =>
                {
                    await sut.GetAchievementsByIds(null);
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Unit")]
        public async Task Achievement_ids_cannot_be_empty()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AchievementService>();

            await Assert.ThrowsAsync<ArgumentException>("achievementIds",
                async () =>
                {
                    await sut.GetAchievementsByIds(new int[0]);
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_achievements_by_page()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AchievementService>();

            var actual = await sut.GetAchievementsByPage(1, 3);

            Assert.Equal(3, actual.Count);
            Assert.Equal(3, actual.PageSize);
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public async Task Page_index_cannot_be_negative()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AchievementService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetAchievementsByPage(-1, 3));
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public async Task Page_size_cannot_be_negative()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AchievementService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetAchievementsByPage(1, -3));
        }
    }
}
