using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.Wvw.Matches.Overview;

namespace GuildWars2.Tests.Features.Wvw.Matches.Overview;

public class MatchOverviewById
{
    [Test]
    public async Task Can_be_found()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        const string id = "1-1";
        (MatchOverview actual, MessageContext context) = await sut.Wvw.GetMatchOverviewById(id, cancellationToken: TestContext.Current!.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
