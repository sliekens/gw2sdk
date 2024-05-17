using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Miniatures;

public class UnlockedMiniatures
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = TestConfiguration.ApiKey;

        var (actual, _) =
            await sut.Hero.Equipment.Miniatures.GetUnlockedMiniatures(accessToken.Key);

        Assert.NotEmpty(actual);
    }
}
