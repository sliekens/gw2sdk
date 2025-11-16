using GuildWars2.Exploration.Regions;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Exploration.Regions;

[ServiceDataSource]
public class RegionById(Gw2Client sut)
{
    [Test]
    [Arguments(1, 0, 1)]
    [Arguments(1, 0, 2)]
    [Arguments(1, 0, 3)]
    public async Task Can_be_found(int continentId, int floorId, int regionId)
    {
        (Region actual, MessageContext context) = await sut.Exploration.GetRegionById(continentId, floorId, regionId, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual.Id).IsEqualTo(regionId);
    }
}
