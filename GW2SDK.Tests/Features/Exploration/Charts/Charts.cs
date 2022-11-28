using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Exploration.Charts;

public class Charts
{
    [Theory]
    [InlineData(1, 0, 1)]
    [InlineData(1, 0, 2)]
    [InlineData(1, 0, 3)]
    public async Task Can_be_enumerated(int continentId, int floorId, int regionId)
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Maps.GetCharts(continentId, floorId, regionId);

        Assert.NotEmpty(actual);
        Assert.Equal(actual.Count, actual.Context.ResultCount);
        Assert.Equal(actual.Count, actual.Context.ResultTotal);
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
