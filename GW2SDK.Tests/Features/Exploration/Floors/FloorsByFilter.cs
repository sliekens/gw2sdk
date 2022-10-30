using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Tests.Features.Exploration.Regions;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Exploration.Floors;

public class FloorsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const int continentId = 1;
        HashSet<int> ids = new()
        {
            0,
            1,
            2
        };

        var actual = await sut.Maps.GetFloorsByIds(continentId, ids);

        Assert.Equal(ids.Count, actual.Count);
        Assert.Equal(ids.Count, actual.Context.ResultCount);
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
