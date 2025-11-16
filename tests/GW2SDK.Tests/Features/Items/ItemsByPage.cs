using GuildWars2.Items;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Items;

[ServiceDataSource]
public class ItemsByPage(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_page()
    {
        const int pageSize = 3;
        (HashSet<Item> actual, MessageContext context) = await sut.Items.GetItemsByPage(0, pageSize, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).Member(c => c.Links!, l => l.IsNotNull())
            .And.Member(c => c.PageSize, ps => ps.IsEqualTo(pageSize))
            .And.Member(c => c.ResultCount, rc => rc.IsEqualTo(pageSize))
            .And.Member(c => c.PageTotal!.Value, pt => pt.IsGreaterThan(0))
            .And.Member(c => c.ResultTotal!.Value, rt => rt.IsGreaterThan(0));
        await Assert.That(actual).HasCount().EqualTo(pageSize)
            .And.All(item => item is not null);
    }
}
