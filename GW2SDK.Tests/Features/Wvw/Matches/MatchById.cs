using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Wvw.Matches;

public class MatchById
{
    [Fact]
    public async Task Can_be_found()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const string matchId = "1-1";

        var actual = await sut.Wvw.GetMatchById(matchId);

        Assert.Equal(matchId, actual.Value.Id);
        actual.Value.has_start_time();
        actual.Value.Has_end_time();
    }
}
