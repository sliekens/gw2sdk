using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Continents;

public class ContinentsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = [1, 2];

        var (actual, context) = await sut.Exploration.GetContinentsByIds(ids);

        Assert.Equal(ids.Count, context.ResultCount);
        Assert.Equal(ids.Count, context.ResultTotal);
        Assert.Equal(ids.Count, actual.Count);
        Assert.Collection(
            ids,
            first => Assert.Contains(actual, found => found.Id == first),
            second => Assert.Contains(actual, found => found.Id == second)
        );
    }
}
