using GuildWars2.Tests.Features.Exploration.Regions;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Floors;

public class FloorById
{
    [Theory]
    [InlineData(1, 0)]
    [InlineData(1, 1)]
    [InlineData(1, 2)]
    [InlineData(2, 1)]
    [InlineData(2, 3)]
    [InlineData(2, 5)]
    public async Task Can_be_found(int continentId, int floorId)
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, _) = await sut.Maps.GetFloorById(continentId, floorId);

        Assert.Equal(floorId, actual.Id);
        actual.Has_texture_dimensions();
        actual.Has_regions();
        foreach (var (regionId, region) in actual.Regions)
        {
            Assert.Equal(regionId, region.Id);
            region.Has_name();
            region.Has_maps();

            // TODO: complete validation
            foreach (var (mapId, map) in region.Maps)
            {
                Assert.Equal(mapId, map.Id);
            }
        }
    }
}
