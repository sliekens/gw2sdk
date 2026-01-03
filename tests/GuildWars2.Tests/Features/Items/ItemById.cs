using GuildWars2.Items;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Items;

[ServiceDataSource]
public class ItemById(Gw2Client sut)
{
    [Test]
    public async Task An_item_can_be_found_by_id()
    {
        const int id = 24;
        (Item actual, MessageContext context) = await sut.Items.GetItemById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual).Member(a => a.Id, idMember => idMember.IsEqualTo(id));
    }
}
