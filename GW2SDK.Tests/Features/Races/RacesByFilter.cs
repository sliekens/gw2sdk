using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Races;

public class RacesByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_name()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<RaceName> names = new()
        {
            RaceName.Asura,
            RaceName.Charr,
            RaceName.Norn
        };

        var actual = await sut.Races.GetRacesByNames(names);

        Assert.Collection(
            names,
            first => Assert.Contains(actual.Value, found => found.Id == first),
            second => Assert.Contains(actual.Value, found => found.Id == second),
            third => Assert.Contains(actual.Value, found => found.Id == third)
        );
    }
}
