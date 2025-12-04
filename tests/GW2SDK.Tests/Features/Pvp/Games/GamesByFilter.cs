using GuildWars2.Pvp.Games;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Pvp.Games;

[ServiceDataSource]
public class GamesByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        // No way other way to get a game ID than to list them all first
        HashSet<string> ids = await sut.Pvp.GetGamesIndex(accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken).ValueOnly();
        // Now that we have game IDs, we can get the games
        (HashSet<Game> actual, MessageContext context) = await sut.Pvp.GetGamesByIds(ids, accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(ids.Count));
            await Assert.That(context).Member(c => c.ResultTotal, rt => rt.IsEqualTo(ids.Count));
            await Assert.That(actual).Count().IsEqualTo(ids.Count);
            foreach (string id in ids)
            {
                await Assert.That(actual.Any(found => found.Id == id)).IsTrue();
            }
        }
    }
}
