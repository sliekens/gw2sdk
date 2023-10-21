using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pvp.Games;

public class GamesByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        // No way other way to get a game ID than to list them all first
        var ids = await sut.Pvp.GetGamesIndex(accessToken.Key);

        // Now that we have game IDs, we can get the games
        var actual = await sut.Pvp.GetGamesByIds(ids.Value, accessToken.Key);

        Assert.Equal(ids.Value.Count, actual.Value.Count);
        Assert.NotNull(actual.ResultContext);
        Assert.Equal(ids.Value.Count, actual.ResultContext.ResultCount);
        Assert.All(
            actual.Value,
            entry =>
            {
                entry.Has_id();
            }
        );
    }
}
