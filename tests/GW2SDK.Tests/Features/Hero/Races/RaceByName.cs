using GuildWars2.Hero;
using GuildWars2.Hero.Races;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Races;

public class RaceByName
{
    [Test]
    public async Task Can_be_found()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        const RaceName name = RaceName.Human;
        (Race actual, _) = await sut.Hero.Races.GetRaceByName(name, cancellationToken: TestContext.Current!.CancellationToken);
        Assert.Equal(name, actual.Id);
    }
}
