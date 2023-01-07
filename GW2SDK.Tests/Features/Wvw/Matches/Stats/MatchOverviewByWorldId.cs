using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Wvw.Matches.Stats;

public class MatchStatsByWorldId
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int worldId = 2006;

        var actual = await sut.Wvw.GetMatchStatsByWorldId(worldId);

        actual.Value.Has_id();
        actual.Value.Has_kills();
        actual.Value.Has_deaths();
        actual.Value.Has_maps();
    }
}
