using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Wvw.Matches.Scores;

namespace GuildWars2.Tests.Features.Wvw.Matches.Scores;

[ServiceDataSource]
public class MatchesScoresByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        HashSet<string> ids = ["1-1", "1-2", "1-3"];
        (IImmutableValueSet<MatchScores> actual, MessageContext context) = await sut.Wvw.GetMatchesScoresByIds(ids, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(ids.Count));
            await Assert.That(context.ResultTotal > ids.Count).IsTrue();
            await Assert.That(actual).Count().IsEqualTo(ids.Count);
            await Assert.That(actual.ElementAt(0).Id).IsIn(ids);
            await Assert.That(actual.ElementAt(1).Id).IsIn(ids);
            await Assert.That(actual.ElementAt(2).Id).IsIn(ids);
        }
    }
}
