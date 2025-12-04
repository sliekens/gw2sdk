using GuildWars2.Hero.Builds;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Builds;

[ServiceDataSource]
public class Builds(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        TestCharacter character = TestConfiguration.TestCharacter;
        ApiKey accessToken = TestConfiguration.ApiKey;
        (HashSet<BuildTemplate> actual, MessageContext context) = await sut.Hero.Builds.GetBuilds(character.Name, accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.Links).IsNotNull();
        await Assert.That(context.PageSize).IsEqualTo(50);
        await Assert.That(context.PageTotal).IsEqualTo(1);
        await Assert.That(context.ResultTotal).IsEqualTo(context.ResultCount);
        await Assert.That(context.ResultCount).IsEqualTo(actual.Count);
        await Assert.That(actual).IsNotEmpty();

        using (Assert.Multiple())
        {
            foreach (BuildTemplate entry in actual)
            {
                await Assert.That(entry).IsNotNull();
                await Assert.That(entry.Build).IsNotNull();
            }
        }
    }
}
