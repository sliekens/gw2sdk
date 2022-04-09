using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Achievements;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements
{
    public class AchievementServiceTest
    {
        [Fact]
        public async Task It_can_get_all_achievement_ids()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AchievementService>();

            var actual = await sut.GetAchievementsIndex();

            Assert.Equal(actual.Context.ResultTotal, actual.Count);
        }

        [Fact]
        public async Task It_can_get_an_achievement_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AchievementService>();

            const int achievementId = 1;

            var actual = await sut.GetAchievementById(achievementId);

            Assert.Equal(achievementId, actual.Value.Id);
        }

        [Fact]
        public async Task It_can_get_achievements_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AchievementService>();

            var achievementIds = new HashSet<int> { 1, 2, 3 };

            var actual = await sut.GetAchievementsByIds(achievementIds);

            Assert.Collection(actual,
                achievement => Assert.Equal(1, achievement.Id),
                achievement => Assert.Equal(2, achievement.Id),
                achievement => Assert.Equal(3, achievement.Id));
        }

        [Fact]
        public async Task It_can_get_achievements_by_page()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AchievementService>();

            var actual = await sut.GetAchievementsByPage(0, 3);

            Assert.Equal(3, actual.Count);
            Assert.Equal(3, actual.Context.PageSize);
        }
    }
}
