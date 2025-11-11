using GuildWars2.Exploration.PointsOfInterest;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Exploration.PointsOfInterest;

[ServiceDataSource]
public class PointsOfInterest(Gw2Client sut)
{
    [Test]
    [Arguments(1, 0, 1, 26)]
    [Arguments(1, 0, 1, 27)]
    [Arguments(1, 0, 1, 28)]
    public async Task Can_be_listed(int continentId, int floorId, int regionId, int mapId)
    {
        (HashSet<PointOfInterest> actual, MessageContext context) = await sut.Exploration.GetPointsOfInterest(continentId, floorId, regionId, mapId, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
        Assert.All(actual, Assert.NotNull);
    }
}
