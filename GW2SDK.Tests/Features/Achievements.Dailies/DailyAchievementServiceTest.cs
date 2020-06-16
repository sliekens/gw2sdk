﻿using System.Threading.Tasks;
using GW2SDK.Achievements.Dailies;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements.Dailies
{
    public class DailyAchievementServiceTest
    {
        [Theory]
        [InlineData(Day.Today)]
        [InlineData(Day.Tomorrow)]
        public async Task Can_get_daily_achievements(Day day)
        {
            await using var services = new Container();
            var sut = services.Resolve<DailyAchievementService>();

            var actual = await sut.GetDailyAchievements(day);

            Assert.IsType<DailyAchievementGroup>(actual);
        }
    }
}
