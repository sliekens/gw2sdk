using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GW2SDK.Features.Achievements;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements
{
    public class AchievementServiceTest
    {
        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public async Task GetAchievementsIndex_ShouldReturnAllIds()
        {
            var services = new Container();
            var sut = services.Resolve<AchievementService>();

            var actual = await sut.GetAchievementsIndex();

            Assert.NotEmpty(actual);
            Assert.Equal(actual.ResultCount, actual.Count);
            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public async Task GetAchievementsByIds_ShouldReturnRequestedAchievement()
        {
            var services = new Container();
            var sut = services.Resolve<AchievementService>();

            var achievementId = 1;

            var actual = await sut.GetAchievementById(achievementId);

            Assert.Equal(achievementId, actual.Id);
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public async Task GetAchievementsByIds_ShouldReturnRequestedAchievements()
        {
            var services = new Container();
            var sut = services.Resolve<AchievementService>();

            var achievementIds = new List<int> { 1, 2, 3 };

            var actual = await sut.GetAchievementsByIds(achievementIds);

            Assert.NotEmpty(actual);
            Assert.Collection(actual,
                achievement => Assert.Equal(1, achievement.Id),
                achievement => Assert.Equal(2, achievement.Id),
                achievement => Assert.Equal(3, achievement.Id));
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Unit")]
        public async Task GetAchievementsByIds_WithIdsNull_ShouldThrowArgumentNullException()
        {
            var services = new Container();
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
        public async Task GetAchievementsByIds_WithIdsEmpty_ShouldThrowArgumentException()
        {
            var services = new Container();
            var sut = services.Resolve<AchievementService>();

            await Assert.ThrowsAsync<ArgumentException>("achievementIds",
                async () =>
                {
                    await sut.GetAchievementsByIds(Enumerable.Empty<int>().ToList());
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public async Task GetAchievementsByPage_ShouldReturnThePage()
        {
            var services = new Container();
            var sut = services.Resolve<AchievementService>();

            var actual = await sut.GetAchievementsByPage(0, 200);

            Assert.NotEmpty(actual);
            Assert.Equal(actual.Count, actual.ResultCount);
            Assert.Equal(200,          actual.PageSize);
        }
    }
}
