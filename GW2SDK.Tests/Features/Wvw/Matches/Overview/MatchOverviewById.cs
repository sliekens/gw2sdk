using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Wvw.Matches.Overview;

public class MatchOverviewById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string id = "1-1";

        var (actual, _) = await sut.Wvw.GetMatchOverviewById(id);

        Assert.Equal(id, actual.Id);
    }
}
