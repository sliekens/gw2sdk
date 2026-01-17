using GuildWars2.Pvp.Amulets;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pvp.Amulets;

[ServiceDataSource]
public class AmuletsByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        HashSet<int> ids = [4, 8, 9];
        (IImmutableValueSet<Amulet> actual, MessageContext context) = await sut.Pvp.GetAmuletsByIds(ids, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(ids.Count));
            await Assert.That(context.ResultTotal!.Value).IsGreaterThan(ids.Count);
            await Assert.That(actual).Count().IsEqualTo(ids.Count);
            await Assert.That(actual.ElementAt(0).Id).IsIn(ids);
            await Assert.That(actual.ElementAt(1).Id).IsIn(ids);
            await Assert.That(actual.ElementAt(2).Id).IsIn(ids);
        }
    }
}
