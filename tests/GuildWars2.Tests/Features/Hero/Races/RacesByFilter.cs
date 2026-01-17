using GuildWars2.Hero;
using GuildWars2.Hero.Races;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Races;

[ServiceDataSource]
public class RacesByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_name()
    {
        HashSet<RaceName> names = [RaceName.Asura, RaceName.Charr, RaceName.Norn];
        (IImmutableValueSet<Race> actual, MessageContext context) = await sut.Hero.Races.GetRacesByNames(names, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.ResultCount).IsEqualTo(names.Count);
        await Assert.That(context.ResultTotal > names.Count).IsTrue();
        await Assert.That(actual.Count).IsEqualTo(names.Count);
        foreach (RaceName name in names)
        {
            await Assert.That(actual).Contains(found => found.Id == name);
        }
    }
}
