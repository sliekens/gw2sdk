using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Exploration.Charts;

public class ChartById
{
    [Theory]
    [InlineData(1, 0, 1, 26)]
    [InlineData(1, 0, 1, 27)]
    [InlineData(1, 0, 1, 28)]
    public async Task Can_be_found(int continentId, int floorId, int regionId, int mapId)
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Maps.GetChartById(continentId, floorId, regionId, mapId);

        // TODO: complete validation
        Assert.Equal(mapId, actual.Value.Id);
        actual.Value.Has_name();
        foreach (var skillChallenge in actual.Value.SkillChallenges)
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
