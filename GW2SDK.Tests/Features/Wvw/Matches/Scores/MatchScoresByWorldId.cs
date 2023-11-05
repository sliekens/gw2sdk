using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Wvw.Matches.Scores;

public class MatchScoresByWorldId
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int worldId = 2006;

        var (actual, _) = await sut.Wvw.GetMatchScoresByWorldId(worldId);

        actual.Has_id();
        actual.Has_scores();
        actual.Has_victory_points();
        actual.Has_skirmishes();
    }
}
