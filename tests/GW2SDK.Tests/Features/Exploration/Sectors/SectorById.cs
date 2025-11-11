using GuildWars2.Exploration.Sectors;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Exploration.Sectors;

[ServiceDataSource]
public class SectorById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int continentId = 1;
        const int floorId = 0;
        const int regionId = 1;
        const int mapId = 26;
        const int sectorId = 513;
        (Sector actual, MessageContext context) = await sut.Exploration.GetSectorById(continentId, floorId, regionId, mapId, sectorId, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(sectorId, actual.Id);
    }
}
