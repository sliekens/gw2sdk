using System.Threading.Tasks;
using GW2SDK.Achievements.Dailies;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements.Dailies
{
    public class DailyAchievementServiceTest
    {
        [Fact]
        public async Task Get_the_daily_achievements()
        {
            await using var services = new Container();
            var sut = services.Resolve<DailyAchievementService>();

            var actual = await sut.GetDailyAchievements();

            Assert.IsType<DailyAchievementGroup>(actual);
        }
    }
}
