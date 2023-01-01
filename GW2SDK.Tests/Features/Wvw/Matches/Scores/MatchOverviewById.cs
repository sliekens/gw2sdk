using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Wvw.Matches.Scores;

public class MatchScoresById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string matchId = "1-1";

        var actual = await sut.Wvw.GetMatchScoresById(matchId);

        Assert.Equal(matchId, actual.Value.Id);
        actual.Value.Has_scores();
        actual.Value.Has_victory_points();
        actual.Value.Has_skirmishes();
    }
}
