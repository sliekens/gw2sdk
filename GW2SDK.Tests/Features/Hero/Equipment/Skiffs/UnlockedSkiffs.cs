using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Skiffs;

public class UnlockedSkiffs
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var (actual, _) = await sut.Hero.Equipment.Skiffs.GetUnlockedSkiffs(accessToken.Key);

        Assert.NotEmpty(actual);
    }
}
