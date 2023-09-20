using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Armory;

public class LegendaryItemsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            83162,
            93105,
            80111
        };

        var actual = await sut.Armory.GetLegendaryItemsByIds(ids);

        Assert.Collection(
            ids,
            first => Assert.Contains(actual.Value, found => found.Id == first),
            second => Assert.Contains(actual.Value, found => found.Id == second),
            third => Assert.Contains(actual.Value, found => found.Id == third)
        );
    }
}
