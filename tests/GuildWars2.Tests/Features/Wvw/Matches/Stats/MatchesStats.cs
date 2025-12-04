using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Wvw.Matches.Stats;

namespace GuildWars2.Tests.Features.Wvw.Matches.Stats;

[ServiceDataSource]
public class MatchesStats(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<MatchStats> actual, MessageContext context) = await sut.Wvw.GetMatchesStats(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        await Assert.That(context).Member(c => c.ResultCount, m => m.IsEqualTo(actual.Count));
        await Assert.That(context).Member(c => c.ResultTotal, m => m.IsEqualTo(actual.Count));
        foreach (MatchStats entry in actual)
        {
            await Assert.That(entry.Id).IsNotEmpty();
            await Assert.That(entry.Kills).IsNotNull();
            await Assert.That(entry.Deaths).IsNotNull();
            await Assert.That(entry.Maps).IsNotEmpty();
            foreach (MapSummary map in entry.Maps)
            {
                await Assert.That(map.Id > 0).IsTrue();
                await Assert.That(map.Kind.IsDefined()).IsTrue();
            }
        }
    }
}
