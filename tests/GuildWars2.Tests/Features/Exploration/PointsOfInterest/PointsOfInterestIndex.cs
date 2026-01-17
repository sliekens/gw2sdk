using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Exploration.PointsOfInterest;

[ServiceDataSource]
public class PointsOfInterestIndex(Gw2Client sut)
{
    [Test]
    [Arguments(1, 0, 1, 26)]
    [Arguments(1, 0, 1, 27)]
    [Arguments(1, 0, 1, 28)]
    public async Task Can_be_listed(int continentId, int floorId, int regionId, int mapId)
    {
        (IImmutableValueSet<int> actual, MessageContext context) = await sut.Exploration.GetPointsOfInterestIndex(continentId, floorId, regionId, mapId, TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(actual.Count))
            .And.Member(c => c.ResultTotal, rt => rt.IsEqualTo(actual.Count));
        await Assert.That(actual).IsNotEmpty();
    }
}
