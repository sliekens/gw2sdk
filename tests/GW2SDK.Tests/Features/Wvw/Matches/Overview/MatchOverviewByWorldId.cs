using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Wvw.Matches.Overview;

namespace GuildWars2.Tests.Features.Wvw.Matches.Overview;

[ServiceDataSource]
public class MatchOverviewByWorldId(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int worldId = 2006;
        // Can fail on a Friday after reset
        // ---> GuildWars2.Http.ResourceNotFoundException : world not currently in a match
        (MatchOverview actual, _) = await sut.Wvw.GetMatchOverviewByWorldId(worldId, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotNull();
    }
}
