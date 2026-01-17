using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Builds;

[ServiceDataSource]
public class BuildNumbers(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        TestCharacter character = TestConfiguration.TestCharacter;
        ApiKey accessToken = TestConfiguration.ApiKey;
        (IImmutableValueSet<int> actual, MessageContext context) = await sut.Hero.Builds.GetBuildNumbers(character.Name, accessToken.Key, TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        await Assert.That(context.ResultCount).IsEqualTo(actual.Count);
        await Assert.That(context.ResultTotal).IsEqualTo(actual.Count);
    }
}
