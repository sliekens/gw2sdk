using GuildWars2.Hero;
using GuildWars2.Hero.Races;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Races;

public class RaceByName
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const RaceName name = RaceName.Human;

        var (actual, _) = await sut.Hero.Races.GetRaceByName(name);

        Assert.Equal(name, actual.Id);
    }
}
