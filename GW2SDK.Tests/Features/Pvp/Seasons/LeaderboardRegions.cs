using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Pvp.Seasons;

public class LeaderboardRegions
{
    [Theory]
    [InlineData("2B2E80D3-0A74-424F-B0EA-E221500B323C", "legendary")]
    [InlineData("2B2E80D3-0A74-424F-B0EA-E221500B323C", "guild")]
    [InlineData("5DD4CF6F-C68B-47E2-8926-8A7D0AE78462", "ladder")]
    public async Task Can_be_found(string seasonId, string boardId)
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Pvp.GetLeaderboardRegions(seasonId, boardId);

        var expected = new HashSet<string>
        {
            "eu",
            "na"
        };

        Assert.Equal(expected, actual.Value);
    }
}
