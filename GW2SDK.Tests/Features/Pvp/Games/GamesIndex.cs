using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pvp.Games;

public class GamesIndex
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = TestConfiguration.ApiKey;

        var (actual, context) = await sut.Pvp.GetGamesIndex(accessToken.Key);

        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
    }
}
