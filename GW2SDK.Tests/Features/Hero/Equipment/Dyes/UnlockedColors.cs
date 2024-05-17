using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Dyes;

public class UnlockedColors
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = TestConfiguration.ApiKey;

        var (actual, _) = await sut.Hero.Equipment.Dyes.GetUnlockedColors(accessToken.Key);

        Assert.NotEmpty(actual);
        Assert.All(actual, id => Assert.NotEqual(0, id));
    }
}
