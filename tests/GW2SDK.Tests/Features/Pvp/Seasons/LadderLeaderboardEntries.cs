using GuildWars2.Pvp.Seasons;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pvp.Seasons;

[ServiceDataSource]
public class LadderLeaderboardEntries(Gw2Client sut)
{
    [Test]
    [Arguments("5DD4CF6F-C68B-47E2-8926-8A7D0AE78462", "ladder", "eu")]
    [Arguments("5DD4CF6F-C68B-47E2-8926-8A7D0AE78462", "ladder", "na")]
    public async Task Can_be_found(string seasonId, string boardId, string regionId)
    {
        (HashSet<LeaderboardEntry> actual, MessageContext context) = await sut.Pvp.GetLeaderboardEntries(seasonId, boardId, regionId, 0, 200, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.NotNull(context.Links);
        Assert.All(actual, entry =>
        {
            Assert.Empty(entry.GuildId);
            Assert.Empty(entry.TeamName);
            Assert.Null(entry.TeamId);
        });
    }
}
