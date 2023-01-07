using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Wvw.Matches.Stats;

public class MatchStatsById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string matchId = "1-1";

        var actual = await sut.Wvw.GetMatchStatsById(matchId);

        Assert.Equal(matchId, actual.Value.Id);
        actual.Value.Has_kills();
        actual.Value.Has_deaths();
        actual.Value.Has_maps();
    }
}
