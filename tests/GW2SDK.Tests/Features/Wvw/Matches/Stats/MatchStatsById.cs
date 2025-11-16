using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Wvw.Matches.Stats;

namespace GuildWars2.Tests.Features.Wvw.Matches.Stats;

[ServiceDataSource]
public class MatchStatsById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const string id = "1-1";
        (MatchStats actual, MessageContext context) = await sut.Wvw.GetMatchStatsById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual.Id).IsEqualTo(id);
    }
}
