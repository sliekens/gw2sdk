using GuildWars2.Hero.Builds;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Builds;

[ServiceDataSource]
public class Build(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        TestCharacter character = TestConfiguration.TestCharacter;
        ApiKey accessToken = TestConfiguration.ApiKey;
        const int tab = 1;
        (BuildTemplate actual, _) = await sut.Hero.Builds.GetBuild(tab, character.Name, accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotNull();
        await Assert.That(actual.Build).IsNotNull();
    }
}
