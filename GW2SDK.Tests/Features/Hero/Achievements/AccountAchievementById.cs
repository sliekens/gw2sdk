using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Achievements;

public class AccountAchievementById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        const int id = 1;

        var (actual, context) =
            await sut.Hero.Achievements.GetAccountAchievementById(id, accessToken.Key);

        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
