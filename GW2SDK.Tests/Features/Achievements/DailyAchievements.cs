using GuildWars2.Http;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Achievements;

public class DailyAchievements
{
    [Theory]
    [InlineData(Day.Today)]
    [InlineData(Day.Tomorrow)]
    public async Task Daily_achievements_can_be_found_by_day(Day day)
    {
        var sut = Composer.Resolve<Gw2Client>();

        var e = await Record.ExceptionAsync(async () =>
        {
            var actual = await sut.Achievements.GetDailyAchievements(day);
        });

        // Unavailable due to Wizard Vault changes
        var reason = Assert.IsType<GatewayException>(e);
        Assert.Equal("API not active", reason.Message);
    }
}
