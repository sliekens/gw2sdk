using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Exploration.Sectors;

[ServiceDataSource]
public class SectorsIndex(Gw2Client sut)
{
    [Test]
    [Arguments(1, 0, 1, 26)]
    [Arguments(1, 0, 1, 27)]
    [Arguments(1, 0, 1, 28)]
    public async Task Can_be_listed(int continentId, int floorId, int regionId, int mapId)
    {
        (HashSet<int> actual, MessageContext context) = await sut.Exploration.GetSectorsIndex(continentId, floorId, regionId, mapId, TestContext.Current!.Execution.CancellationToken);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
    }
}
