using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

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

        var actual = await sut.Maps.GetRegionById(continentId, floorId, regionId);

        Assert.Equal(regionId, actual.Value.Id);
        actual.Value.Has_name();
        actual.Value.Has_maps();
        foreach (var (mapId, map) in actual.Value.Maps)
        {
            // TODO: complete validation
            Assert.Equal(mapId, map.Id);
        }
    }
}
