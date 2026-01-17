using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Wvw.Matches;

namespace GuildWars2.Tests.Features.Wvw.Matches;

[ServiceDataSource]
public class MatchesByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        HashSet<string> ids = ["1-1", "1-2", "1-3"];
        (IImmutableValueSet<Match> actual, MessageContext context) = await sut.Wvw.GetMatchesByIds(ids, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(ids.Count));
            await Assert.That(context.ResultTotal > ids.Count).IsTrue();
            await Assert.That(actual).Count().IsEqualTo(ids.Count);
            foreach (string id in ids)
            {
                await Assert.That(actual).Contains(found => found.Id == id);
            }
        }
    }
}
