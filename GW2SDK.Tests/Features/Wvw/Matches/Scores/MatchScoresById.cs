using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Wvw.Matches.Scores;

public class MatchScoresById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string id = "1-1";

        var (actual, _) = await sut.Wvw.GetMatchScoresById(id);

        Assert.Equal(id, actual.Id);
    }
}
