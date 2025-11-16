using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Wvw.Matches;
using GuildWars2.Wvw.Matches.Scores;

namespace GuildWars2.Tests.Features.Wvw.Matches.Scores;

[ServiceDataSource]
public class MatchesScores(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<MatchScores> actual, MessageContext context) = await sut.Wvw.GetMatchesScores(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        await Assert.That(context).Member(c => c.ResultCount, m => m.IsEqualTo(actual.Count));
        await Assert.That(context).Member(c => c.ResultTotal, m => m.IsEqualTo(actual.Count));
        foreach (MatchScores entry in actual)
        {
            await Assert.That(entry.Id).IsNotEmpty();
            foreach (Skirmish skirmish in entry.Skirmishes)
            {
                await Assert.That(skirmish.Id > 0).IsTrue();
                foreach (MapScores score in skirmish.MapScores)
                {
                    await Assert.That(score.Kind.IsDefined()).IsTrue();
                }
            }
            foreach (MapSummary map in entry.Maps)
            {
                await Assert.That(map.Id > 0).IsTrue();
                await Assert.That(map.Kind.IsDefined()).IsTrue();
            }
        }
    }
}
