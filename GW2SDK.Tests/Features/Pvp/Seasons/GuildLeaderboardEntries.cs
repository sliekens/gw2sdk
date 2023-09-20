using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pvp.Seasons;

public class GuildLeaderboardEntries
{
    [Theory]
    [InlineData("2B2E80D3-0A74-424F-B0EA-E221500B323C", "guild", "eu")]
    [InlineData("2B2E80D3-0A74-424F-B0EA-E221500B323C", "guild", "na")]
    public async Task Can_be_found(string seasonId, string boardId, string regionId)
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Pvp.GetLeaderboardEntries(seasonId, boardId, regionId, 0, 200);

        Assert.NotEmpty(actual.Value);
        Assert.NotNull(actual.ResultContext);
        Assert.NotNull(actual.PageContext);
    }
}
