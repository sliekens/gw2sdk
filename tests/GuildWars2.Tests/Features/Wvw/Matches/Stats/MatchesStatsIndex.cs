using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Wvw.Matches.Stats;

[ServiceDataSource]
public class MatchesStatsIndex(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<string> actual, MessageContext context) = await sut.Wvw.GetMatchesStatsIndex(TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).Member(c => c.ResultCount, m => m.IsEqualTo(actual.Count));
        await Assert.That(context).Member(c => c.ResultTotal, m => m.IsEqualTo(actual.Count));
        await Assert.That(actual).IsNotEmpty();
    }
}
