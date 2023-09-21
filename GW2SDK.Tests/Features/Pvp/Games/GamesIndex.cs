using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pvp.Games;

public class GamesIndex
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Pvp.GetGamesIndex(accessToken.Key);

        Assert.NotEmpty(actual.Value);
        Assert.Equal(actual.Value.Count, actual.ResultContext.ResultCount);
        Assert.Equal(actual.Value.Count, actual.ResultContext.ResultTotal);
    }
}
