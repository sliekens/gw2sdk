using GuildWars2.Items;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Items;

public class ItemById
{
    [Test]
    public async Task An_item_can_be_found_by_id()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        const int id = 24;
        (Item actual, MessageContext context) = await sut.Items.GetItemById(id, cancellationToken: TestContext.Current!.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
