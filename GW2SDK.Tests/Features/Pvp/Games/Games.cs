using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pvp.Games;

public class Games
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = TestConfiguration.ApiKey;

        var (actual, context) = await sut.Pvp.GetGames(accessToken.Key);

        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);

        Assert.All(
            actual,
            entry =>
            {
                Assert.NotEmpty(entry.Id);
                Assert.True(entry.RatingType.IsDefined());
                Assert.True(entry.Result.IsDefined());
                Assert.True(entry.Profession.IsDefined());
            }
        );
    }
}
