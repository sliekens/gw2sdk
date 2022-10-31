using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Exploration.Regions;

public class RegionsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const int continentId = 1;
        const int floorId = 0;
        HashSet<int> ids = new()
        {
            1,
            2,
            3
        };

        var actual = await sut.Maps.GetRegionsByIds(continentId, floorId, ids);

        Assert.Equal(ids.Count, actual.Count);
        Assert.Equal(ids.Count, actual.Context.ResultCount);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_id();
                entry.Has_name();
                entry.Has_maps();
                foreach (var (mapId, map) in entry.Maps)
                {
                    // TODO: complete validation
                    Assert.Equal(mapId, map.Id);
                    foreach (var skillChallenge in map.SkillChallenges)
                    {
                        // BUG(?): Cantha (id 37) does not have skill challenge ids
                        if (entry.Id == 37)
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
        );
    }
}
