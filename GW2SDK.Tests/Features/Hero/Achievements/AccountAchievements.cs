using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Achievements;

public class AccountAchievements
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var (actual, context) = await sut.Hero.Achievements.GetAccountAchievements(accessToken.Key);

        Assert.Equal(context.ResultTotal, actual.Count);

        Assert.All(
            actual,
            achievement =>
            {
                achievement.Has_id();
            }
        );
    }
}
