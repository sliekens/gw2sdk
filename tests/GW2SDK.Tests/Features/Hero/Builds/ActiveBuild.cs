using GuildWars2.Hero.Builds;

using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Hero.Builds;

public class ActiveBuild
{

    [Test]

    public async Task Can_be_found()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        TestCharacter character = TestConfiguration.TestCharacter;

        ApiKey accessToken = TestConfiguration.ApiKey;

        (BuildTemplate actual, _) = await sut.Hero.Builds.GetActiveBuild(character.Name, accessToken.Key, cancellationToken: TestContext.Current!.CancellationToken);

        Assert.NotNull(actual);

        Assert.NotNull(actual.Build);
    }
}
