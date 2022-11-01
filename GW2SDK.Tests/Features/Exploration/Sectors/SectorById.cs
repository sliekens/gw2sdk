using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Exploration.Sectors;

public class SectorById
{
    [Fact]
    public async Task Can_be_found()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const int continentId = 1;
        const int floorId = 0;
        const int regionId = 1;
        const int mapId = 26;
        const int sectorId = 513;

        var actual = await sut.Maps.GetSectorById(
            continentId,
            floorId,
            regionId,
            mapId,
            sectorId
        );

        Assert.Equal(sectorId, actual.Value.Id);
    }
}
