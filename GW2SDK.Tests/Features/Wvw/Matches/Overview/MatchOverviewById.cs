using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Wvw.Matches.Overview;

public class MatchOverviewById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string matchId = "1-1";

        var actual = await sut.Wvw.GetMatchOverviewById(matchId);

        Assert.Equal(matchId, actual.Value.Id);
        actual.Value.Has_start_time();
        actual.Value.Has_end_time();
    }
}
