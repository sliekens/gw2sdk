using GuildWars2.Pve.MapChests;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pve.MapChests;

[ServiceDataSource]
public class MapChestsByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        HashSet<string> ids = ["auric_basin_heros_choice_chest", "crystal_oasis_heros_choice_chest", "domain_of_vabbi_heros_choice_chest"];
        (HashSet<MapChest> actual, MessageContext context) = await sut.Pve.MapChests.GetMapChestsByIds(ids, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.Equal(ids.Count, context.ResultCount);
        Assert.True(context.ResultTotal > ids.Count);
        Assert.Equal(ids.Count, actual.Count);
        Assert.Collection(ids, first => Assert.Contains(actual, found => found.Id == first), second => Assert.Contains(actual, found => found.Id == second), third => Assert.Contains(actual, found => found.Id == third));
    }
}
