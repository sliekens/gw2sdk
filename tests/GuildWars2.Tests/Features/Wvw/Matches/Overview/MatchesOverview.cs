using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Wvw.Matches.Overview;

namespace GuildWars2.Tests.Features.Wvw.Matches.Overview;

[ServiceDataSource]
public class MatchesOverview(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<MatchOverview> actual, MessageContext context) = await sut.Wvw.GetMatchesOverview(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        await Assert.That(context).Member(c => c.ResultCount, m => m.IsEqualTo(actual.Count));
        await Assert.That(context).Member(c => c.ResultTotal, m => m.IsEqualTo(actual.Count));
        foreach (MatchOverview entry in actual)
        {
            await Assert.That(entry.Id).IsNotEmpty();
            await Assert.That(entry.StartTime > DateTimeOffset.MinValue).IsTrue();
            await Assert.That(entry.EndTime > entry.StartTime).IsTrue();
        }
    }
}
