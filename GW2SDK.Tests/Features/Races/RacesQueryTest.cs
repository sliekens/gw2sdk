using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Races;

public class RacesQueryTest
{
    [Fact]
    public async Task Races_can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Races.GetRaces();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }

    [Fact]
    public async Task Races_index_is_not_empty()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Races.GetRacesIndex();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }

    [Fact]
    public async Task A_race_can_be_found_by_name()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const RaceName name = RaceName.Human;

        var actual = await sut.Races.GetRaceByName(name);

        Assert.Equal(name, actual.Value.Id);
    }

    [Fact]
    public async Task Races_can_be_filtered_by_name()
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

    [Fact]
    public async Task Races_can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Races.GetRacesByPage(0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.Equal(3, actual.PageContext.PageSize);
    }
}
