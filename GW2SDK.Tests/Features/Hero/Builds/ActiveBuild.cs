using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Builds;

public class ActiveBuild
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = TestConfiguration.TestCharacter;
        var accessToken = TestConfiguration.ApiKey;

        var (actual, _) = await sut.Hero.Builds.GetActiveBuild(character.Name, accessToken.Key, cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(actual);
        Assert.NotNull(actual.Build);
    }
}
