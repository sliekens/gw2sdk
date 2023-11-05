using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Minipets;

public class UnlockedMinipets
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var (actual, _) = await sut.Minipets.GetUnlockedMinipets(accessToken.Key);

        Assert.NotEmpty(actual);
    }
}
