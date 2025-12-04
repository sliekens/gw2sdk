using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pvp.Seasons;

[ServiceDataSource]
public class LeaderboardRegions(Gw2Client sut)
{
    [Test]
    [Arguments("2B2E80D3-0A74-424F-B0EA-E221500B323C", "legendary")]
    [Arguments("2B2E80D3-0A74-424F-B0EA-E221500B323C", "guild")]
    [Arguments("5DD4CF6F-C68B-47E2-8926-8A7D0AE78462", "ladder")]
    public async Task Can_be_found(string seasonId, string boardId)
    {
        (HashSet<string> actual, _) = await sut.Pvp.GetLeaderboardRegions(seasonId, boardId, TestContext.Current!.Execution.CancellationToken);
        HashSet<string> expected = ["eu", "na"];
        using (Assert.Multiple())
        {
            await Assert.That(actual).IsEquivalentTo(expected);
        }
    }
}
