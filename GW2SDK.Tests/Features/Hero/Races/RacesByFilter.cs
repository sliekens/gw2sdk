using GuildWars2.Hero;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Races;

public class RacesByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_name()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<RaceName> names =
        [
            RaceName.Asura,
            RaceName.Charr,
            RaceName.Norn
        ];

        var (actual, context) = await sut.Hero.Races.GetRacesByNames(names, cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal(names.Count, context.ResultCount);
        Assert.True(context.ResultTotal > names.Count);
        Assert.Equal(names.Count, actual.Count);
        Assert.Collection(
            names,
            first => Assert.Contains(actual, found => found.Id == first),
            second => Assert.Contains(actual, found => found.Id == second),
            third => Assert.Contains(actual, found => found.Id == third)
        );
    }
}
