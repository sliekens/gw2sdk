using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Wvw.Matches.Overview;

namespace GuildWars2.Tests.Features.Wvw.Matches.Overview;

[ServiceDataSource]
public class MatchOverviewById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const string id = "1-1";
        (MatchOverview actual, MessageContext context) = await sut.Wvw.GetMatchOverviewById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
