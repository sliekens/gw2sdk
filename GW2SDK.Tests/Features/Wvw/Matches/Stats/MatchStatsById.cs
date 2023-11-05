using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Wvw.Matches.Stats;

public class MatchStatsById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string id = "1-1";

        var (actual, _) = await sut.Wvw.GetMatchStatsById(id);

        Assert.Equal(id, actual.Id);
        actual.Has_kills();
        actual.Has_deaths();
        actual.Has_maps();
    }
}
