using GuildWars2.Pvp.Ranks;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pvp.Ranks;

[ServiceDataSource]
public class RanksByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        HashSet<int> ids = [4, 8, 9];
        (IImmutableValueSet<Rank> actual, MessageContext context) = await sut.Pvp.GetRanksByIds(ids, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(context).Member(c => c.ResultCount, m => m.IsEqualTo(ids.Count));
            await Assert.That(context.ResultTotal!.Value).IsGreaterThan(ids.Count);
            await Assert.That(actual).Count().IsEqualTo(ids.Count);
            foreach (int id in ids)
            {
                await Assert.That(actual).Contains(found => found.Id == id);
            }
        }
    }
}
