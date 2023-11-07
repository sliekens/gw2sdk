using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Miniatures;

public class UnlockedMinipets
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var (actual, _) = await sut.Hero.Miniatures.GetUnlockedMinipets(accessToken.Key);

        Assert.NotEmpty(actual);
    }
}
