using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pvp.Seasons;

[ServiceDataSource]
public class Leaderboards(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const string id = "2B2E80D3-0A74-424F-B0EA-E221500B323C";
        (HashSet<string> actual, _) = await sut.Pvp.GetLeaderboardIds(id, TestContext.Current!.Execution.CancellationToken);
        HashSet<string> expected = ["guild", "legendary"];
        using (Assert.Multiple())
        {
            await Assert.That(actual).IsEquivalentTo(expected);
        }
    }
}
