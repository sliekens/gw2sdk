using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Regions;

public class RegionById
{
    [Theory]
    [InlineData(1, 0, 1)]
    [InlineData(1, 0, 2)]
    [InlineData(1, 0, 3)]
    public async Task Can_be_found(int continentId, int floorId, int regionId)
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, _) = await sut.Maps.GetRegionById(continentId, floorId, regionId);

        Assert.Equal(regionId, actual.Id);
        actual.Has_name();
        actual.Has_maps();
        foreach (var (mapId, map) in actual.Maps)
        {
            // TODO: complete validation
            Assert.Equal(mapId, map.Id);
        }
    }
}
