using GuildWars2.Pvp.Games;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pvp.Games;

public class GamesByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        ApiKey accessToken = TestConfiguration.ApiKey;

        // No way other way to get a game ID than to list them all first
        HashSet<string> ids = await sut.Pvp.GetGamesIndex(
                accessToken.Key,
                cancellationToken: TestContext.Current.CancellationToken
            )
            .ValueOnly();

        // Now that we have game IDs, we can get the games
        (HashSet<Game> actual, MessageContext context) = await sut.Pvp.GetGamesByIds(
            ids,
            accessToken.Key,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.Equal(ids.Count, context.ResultCount);
        Assert.Equal(ids.Count, context.ResultTotal);
        Assert.Equal(ids.Count, actual.Count);
        Assert.Collection(
            ids,
            first => Assert.Contains(actual, found => found.Id == first),
            second => Assert.Contains(actual, found => found.Id == second),
            third => Assert.Contains(actual, found => found.Id == third),
            fourth => Assert.Contains(actual, found => found.Id == fourth),
            fifth => Assert.Contains(actual, found => found.Id == fifth),
            sixth => Assert.Contains(actual, found => found.Id == sixth),
            seventh => Assert.Contains(actual, found => found.Id == seventh),
            eighth => Assert.Contains(actual, found => found.Id == eighth),
            nineth => Assert.Contains(actual, found => found.Id == nineth),
            tenth => Assert.Contains(actual, found => found.Id == tenth)
        );
    }
}
