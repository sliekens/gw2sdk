using GuildWars2.Hero;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Races;

[ServiceDataSource]
public class RacesIndex(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<Extensible<RaceName>> actual, MessageContext context) = await sut.Hero.Races.GetRacesIndex(TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.ResultCount).IsEqualTo(actual.Count);
        await Assert.That(context.ResultTotal).IsEqualTo(actual.Count);
        await Assert.That(actual).IsNotEmpty();
    }
}
