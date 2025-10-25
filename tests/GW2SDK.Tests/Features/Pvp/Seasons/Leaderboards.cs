using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pvp.Seasons;

public class Leaderboards
{
    [Test]
    public async Task Can_be_found()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        const string id = "2B2E80D3-0A74-424F-B0EA-E221500B323C";
        (HashSet<string> actual, _) = await sut.Pvp.GetLeaderboardIds(id, TestContext.Current!.CancellationToken);
        HashSet<string> expected = ["guild", "legendary"];
        Assert.Equal(expected, actual);
    }
}
