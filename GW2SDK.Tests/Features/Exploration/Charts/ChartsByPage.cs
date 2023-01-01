using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Exploration.Charts;

public class ChartsByPage
{
    [Fact]
    public async Task Can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int continentId = 1;
        const int floorId = 0;
        const int regionId = 1;

        var actual = await sut.Maps.GetChartsByPage(continentId, floorId, regionId, 0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.Equal(3, actual.PageContext.PageSize);
        Assert.All(
            actual.Value,
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
