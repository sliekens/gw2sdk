using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Wvw.Matches;

namespace GuildWars2.Tests.Features.Wvw.Matches;

[ServiceDataSource]
public class MatchByWorldId(Gw2Client sut)
{
    [Retry(3, RetryOnExceptionTypes = new[] { typeof(System.Net.Http.HttpRequestException) })]
    [Test]
    public async Task Can_be_found()
    {
        const int worldId = 2006;
        (Match actual, _) = await sut.Wvw.GetMatchByWorldId(worldId, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotNull();
    }
}
