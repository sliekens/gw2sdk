using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Builds;

public class Build
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = TestConfiguration.TestCharacter;
        var accessToken = TestConfiguration.ApiKey;

        const int tab = 1;
        var (actual, _) = await sut.Hero.Builds.GetBuild(tab, character.Name, accessToken.Key);

        Assert.NotNull(actual);
        Assert.NotNull(actual.Build);
    }
}
