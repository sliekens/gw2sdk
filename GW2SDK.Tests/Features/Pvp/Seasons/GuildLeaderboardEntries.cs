using GuildWars2.Tests.TestInfrastructure;
using Xunit.Abstractions;

namespace GuildWars2.Tests.Features.Pvp.Seasons;

public class GuildLeaderboardEntries(ITestOutputHelper outputHelper)
{
    [Theory]
    [InlineData("2B2E80D3-0A74-424F-B0EA-E221500B323C", "guild", "eu")]
    [InlineData("2B2E80D3-0A74-424F-B0EA-E221500B323C", "guild", "na")]
    public async Task Can_be_found(string seasonId, string boardId, string regionId)
    {
        LoggingHandler.Output.Value = outputHelper;
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) =
            await sut.Pvp.GetLeaderboardEntries(seasonId, boardId, regionId, 0, 200);

        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.NotNull(context.Links);

        Assert.All(
            actual,
            entry =>
            {
                Assert.NotEmpty(entry.GuildId);
            }
        );
    }
}
