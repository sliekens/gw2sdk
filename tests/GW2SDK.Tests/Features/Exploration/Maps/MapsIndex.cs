using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Exploration.Maps;

[ServiceDataSource]
public class MapsIndex(Gw2Client sut)
{
    [Test]
    [Arguments(1, 0, 1)]
    [Arguments(1, 0, 2)]
    [Arguments(1, 0, 3)]
    public async Task Map_ids_in_a_region_can_be_listed(int continentId, int floorId, int regionId)
    {
        (HashSet<int> actual, MessageContext context) = await sut.Exploration.GetMapsIndex(continentId, floorId, regionId, TestContext.Current!.Execution.CancellationToken);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
    }

    [Test]
    public async Task All_map_ids_can_be_listed()
    {
        (HashSet<int> actual, MessageContext context) = await sut.Exploration.GetMapsIndex(TestContext.Current!.Execution.CancellationToken);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
    }
}
