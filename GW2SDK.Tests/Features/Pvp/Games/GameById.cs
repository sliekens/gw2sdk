using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pvp.Games;

public class GameById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = TestConfiguration.ApiKey;

        // No way other way to get a game ID than to list them all first
        var gamesIndex = await sut.Pvp.GetGamesIndex(accessToken.Key, cancellationToken: TestContext.Current.CancellationToken).ValueOnly();
        var gameId = gamesIndex.First();

        // Now that we have a game ID, we can get the game
        var (actual, context) = await sut.Pvp.GetGameById(gameId, accessToken.Key, cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(context);
        Assert.Equal(gameId, actual.Id);
    }
}
