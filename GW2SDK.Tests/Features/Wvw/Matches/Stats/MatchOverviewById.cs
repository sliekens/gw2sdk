using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Wvw.Matches.Stats;

public class MatchStatsById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string id = "1-1";

        var actual = await sut.Wvw.GetMatchStatsById(id);

        Assert.Equal(id, actual.Value.Id);
        actual.Value.Has_kills();
        actual.Value.Has_deaths();
        actual.Value.Has_maps();
    }
}
