using GuildWars2.Tests.TestInfrastructure;

using GuildWars2.Wvw.Matches.Overview;


namespace GuildWars2.Tests.Features.Wvw.Matches.Overview;

public class MatchOverviewByWorldId
{

    [Test]

    public async Task Can_be_found()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        const int worldId = 2006;
        // Can fail on a Friday after reset
        // ---> GuildWars2.Http.ResourceNotFoundException : world not currently in a match

        (MatchOverview actual, _) = await sut.Wvw.GetMatchOverviewByWorldId(worldId, cancellationToken: TestContext.Current!.CancellationToken);

        Assert.NotNull(actual);
    }
}
