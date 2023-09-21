using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pvp.Games;

public class GameById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        // No way other way to get a game ID than to list them all first
        var gamesIndex = await sut.Pvp.GetGamesIndex(accessToken.Key);
        var game = gamesIndex.Value.First();

        // Now that we have a game ID, we can get the game
        var actual = await sut.Pvp.GetGameById(game, accessToken.Key);

        Assert.Equal(game, actual.Value.Id);
    }
}
