using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Exploration.Maps;

public class MapsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const int continentId = 1;
        const int floorId = 0;
        const int regionId = 1;
        HashSet<int> ids = new()
        {
            26,
            27,
            28
        };

        var actual = await sut.Maps.GetMapsByIds(continentId, floorId, regionId, ids);

        Assert.Equal(ids.Count, actual.Count);
        Assert.Equal(ids.Count, actual.Context.ResultCount);
        Assert.All(
            actual,
            entry =>
            {
                // TODO: complete validation
                entry.Has_id();
                entry.Has_name();
                foreach (var skillChallenge in entry.SkillChallenges)
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
        );
    }
}
