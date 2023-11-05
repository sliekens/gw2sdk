using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Wvw.Matches;

public class MatchByWorldId
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int worldId = 2006;

        var (actual, _) = await sut.Wvw.GetMatchByWorldId(worldId);

        actual.Has_id();
        actual.Has_start_time();
        actual.Has_end_time();

        // Can fail on a Friday after reset (which is a bit odd, you'd expect no result from GetMatchByWorldId)
        actual.Includes_world(worldId);
    }
}
