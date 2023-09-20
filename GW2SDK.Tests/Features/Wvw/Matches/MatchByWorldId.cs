using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Wvw.Matches;

public class MatchByWorldId
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int worldId = 2006;

        var actual = await sut.Wvw.GetMatchByWorldId(worldId);

        actual.Value.Has_id();
        actual.Value.Has_start_time();
        actual.Value.Has_end_time();
        actual.Value.Includes_world(worldId);
    }
}
