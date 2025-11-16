using GuildWars2.Hero;
using GuildWars2.Hero.Races;

using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Races;

[ServiceDataSource]
public class RaceByName(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const RaceName name = RaceName.Human;
        (Race actual, _) = await sut.Hero.Races.GetRaceByName(name, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual.Id).IsEqualTo(name);
    }
}
