using GuildWars2.Hero.Races;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Races;

[ServiceDataSource]
public class Races(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<Race> actual, MessageContext context) = await sut.Hero.Races.GetRaces(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.ResultTotal).IsEqualTo(actual.Count);
    }
}
