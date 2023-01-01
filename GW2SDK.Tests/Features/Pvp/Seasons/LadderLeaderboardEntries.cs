using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Pvp.Seasons;

public class LadderLeaderboardEntries
{
    [Theory]
    [InlineData("5DD4CF6F-C68B-47E2-8926-8A7D0AE78462", "ladder", "eu")]
    [InlineData("5DD4CF6F-C68B-47E2-8926-8A7D0AE78462", "ladder", "na")]
    public async Task Can_be_found(string seasonId, string boardId, string regionId)
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Pvp.GetLeaderboardEntries(seasonId, boardId, regionId, 0, 200);

        Assert.NotEmpty(actual.Value);
        Assert.NotNull(actual.ResultContext);
        Assert.NotNull(actual.PageContext);
    }
}