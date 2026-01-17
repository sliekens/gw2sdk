using GuildWars2.Items;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Items;

[ServiceDataSource]
public class ItemsByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        HashSet<int> ids = [24, 46, 56];
        (IImmutableValueSet<Item> actual, MessageContext context) = await sut.Items.GetItemsByIds(ids, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(ids.Count))
            .And.Member(c => c.ResultTotal!.Value, rt => rt.IsGreaterThan(ids.Count));
        await Assert.That(actual).Count().IsEqualTo(ids.Count);
        await Assert.That(actual).Any(item => item.Id == 24);
        await Assert.That(actual).Any(item => item.Id == 46);
        await Assert.That(actual).Any(item => item.Id == 56);
    }
}
