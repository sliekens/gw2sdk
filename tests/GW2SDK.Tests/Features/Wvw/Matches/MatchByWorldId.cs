using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Wvw.Matches;

namespace GuildWars2.Tests.Features.Wvw.Matches;

[ServiceDataSource]
public class MatchByWorldId(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int worldId = 2006;
        (Match actual, _) = await sut.Wvw.GetMatchByWorldId(worldId, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(actual);
    }
}
