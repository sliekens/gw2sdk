using System.Threading.Tasks;
using GW2SDK.Tests.Features.Exploration.Regions;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Exploration.Floors;

public class FloorsByPage
{
    [Fact]
    public async Task Can_be_filtered_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const int continentId = 1;

        var actual = await sut.Maps.GetFloorsByPage(continentId, 0, 3);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, actual.Context.PageSize);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_texture_dimensions();
                entry.Has_regions();
                foreach (var (regionId, region) in entry.Regions)
                {
                    Assert.Equal(regionId, region.Id);
                    region.Has_name();
                    region.Has_maps();

                    // TODO: complete validation
                    foreach (var (mapId, map) in region.Maps)
                    {
                        Assert.Equal(mapId, map.Id);
                        foreach (var skillChallenge in map.SkillChallenges)
                        {
                            // BUG(?): Cantha (id 37) does not have skill challenge ids
                            if (regionId == 37)
                            {
                                Assert.Empty(skillChallenge.Id);
                            }
                            else
                            {
                                Assert.NotEmpty(skillChallenge.Id);
                            }
                        }
                    }
                }
            }
        );
    }
}
