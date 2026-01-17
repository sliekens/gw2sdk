using GuildWars2.Pvp.Games;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Pvp.Games;

[ServiceDataSource]
public class GameById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        // No way other way to get a game ID than to list them all first
        IImmutableValueSet<string> gamesIndex = await sut.Pvp.GetGamesIndex(accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken).ValueOnly();
        string? gameId = gamesIndex.First();
        // Now that we have a game ID, we can get the game
        (Game actual, MessageContext context) = await sut.Pvp.GetGameById(gameId, accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(context).IsNotNull();
            await Assert.That(actual.Id).IsEqualTo(gameId);
        }
    }
}
