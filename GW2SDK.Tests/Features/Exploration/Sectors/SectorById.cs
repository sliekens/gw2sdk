using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Sectors;

public class SectorById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int continentId = 1;
        const int floorId = 0;
        const int regionId = 1;
        const int mapId = 26;
        const int sectorId = 513;

        var (actual, _) = await sut.Exploration.GetSectorById(continentId, floorId, regionId, mapId, sectorId);

        Assert.Equal(sectorId, actual.Id);
    }
}
