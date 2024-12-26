using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.MapChests;

public class MapChestsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<string> ids =
        [
            "auric_basin_heros_choice_chest",
            "crystal_oasis_heros_choice_chest",
            "domain_of_vabbi_heros_choice_chest"
        ];

        var (actual, context) = await sut.Pve.MapChests.GetMapChestsByIds(ids, cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal(ids.Count, context.ResultCount);
        Assert.True(context.ResultTotal > ids.Count);
        Assert.Equal(ids.Count, actual.Count);
        Assert.Collection(
            ids,
            first => Assert.Contains(actual, found => found.Id == first),
            second => Assert.Contains(actual, found => found.Id == second),
            third => Assert.Contains(actual, found => found.Id == third)
        );
    }
}
