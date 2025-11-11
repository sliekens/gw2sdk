using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Wvw.Matches.Stats;

namespace GuildWars2.Tests.Features.Wvw.Matches.Stats;

[ServiceDataSource]
public class MatchStatsByWorldId(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int worldId = 2006;
        (MatchStats actual, _) = await sut.Wvw.GetMatchStatsByWorldId(worldId, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(actual);
    }
}
