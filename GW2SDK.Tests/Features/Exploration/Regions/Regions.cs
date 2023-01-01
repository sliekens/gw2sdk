using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Exploration.Regions;

public class Regions
{
    [Theory]
    [InlineData(1, 0)]
    [InlineData(2, 1)]
    public async Task Can_be_enumerated(int continentId, int floorId)
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Maps.GetRegions(continentId, floorId);

        Assert.NotEmpty(actual.Value);
        Assert.Equal(actual.Value.Count, actual.ResultContext.ResultCount);
        Assert.Equal(actual.Value.Count, actual.ResultContext.ResultTotal);
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
