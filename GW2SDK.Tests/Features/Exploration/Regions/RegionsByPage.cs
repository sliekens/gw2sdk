using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Exploration.Regions;

public class RegionsByPage
{
    [Fact]
    public async Task Can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int continentId = 1;
        const int floorId = 1;

        var actual = await sut.Maps.GetRegionsByPage(continentId, floorId, 0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.Equal(3, actual.PageContext.PageSize);
        Assert.All(
            actual.Value,
            entry =>
            {
                entry.Has_id();
                entry.Has_name();
                entry.Has_maps();
                foreach (var (mapId, map) in entry.Maps)
                {
                    // TODO: complete validation
                    Assert.Equal(mapId, map.Id);
                }
            }
        );
    }
}
