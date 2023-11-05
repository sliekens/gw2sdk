using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Races;

public class RaceByName
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const RaceName name = RaceName.Human;

        var (actual, _) = await sut.Races.GetRaceByName(name);

        Assert.Equal(name, actual.Id);
    }
}
