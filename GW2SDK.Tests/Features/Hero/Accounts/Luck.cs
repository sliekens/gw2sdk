using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Accounts;

public class Luck
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var (actual, _) = await sut.Hero.Account.GetLuck(accessToken.Key);

        Assert.True(actual.Luck > 0, "You have no luck today.");
        Assert.Equal(300, actual.MagicFind);
        Assert.True(actual.MagicFindWithoutLevelCap >= actual.MagicFind);
    }
}
