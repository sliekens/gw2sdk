using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Wvw.Matches.Scores;

public class MatchScoresByWorldId
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int worldId = 2006;

        var actual = await sut.Wvw.GetMatchScoresByWorldId(worldId);

        actual.Value.Has_id();
        actual.Value.Has_scores();
        actual.Value.Has_victory_points();
        actual.Value.Has_skirmishes();
    }
}
