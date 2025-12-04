using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Wvw.Matches.Scores;

[ServiceDataSource]
public class MatchesScoresIndex(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<string> actual, MessageContext context) = await sut.Wvw.GetMatchesScoresIndex(TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).Member(c => c.ResultCount, m => m.IsEqualTo(actual.Count));
        await Assert.That(context).Member(c => c.ResultTotal, m => m.IsEqualTo(actual.Count));
        await Assert.That(actual).IsNotEmpty();
    }
}
