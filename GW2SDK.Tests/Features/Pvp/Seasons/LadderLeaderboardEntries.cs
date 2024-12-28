using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pvp.Seasons;

public class LadderLeaderboardEntries
{
    [Theory]
    [InlineData("5DD4CF6F-C68B-47E2-8926-8A7D0AE78462", "ladder", "eu")]
    [InlineData("5DD4CF6F-C68B-47E2-8926-8A7D0AE78462", "ladder", "na")]
    public async Task Can_be_found(string seasonId, string boardId, string regionId)
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Pvp.GetLeaderboardEntries(
            seasonId,
            boardId,
            regionId,
            0,
            200,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.NotNull(context.Links);

        Assert.All(
            actual,
            entry =>
            {
                Assert.Empty(entry.GuildId);
                Assert.Empty(entry.TeamName);
                Assert.Null(entry.TeamId);
            }
        );
    }
}
