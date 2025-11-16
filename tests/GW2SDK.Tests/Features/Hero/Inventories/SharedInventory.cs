using GuildWars2.Hero.Inventories;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Inventories;

[ServiceDataSource]
public class SharedInventory(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (GuildWars2.Hero.Inventories.Inventory actual, _) = await sut.Hero.Inventory.GetSharedInventory(accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual.Items).IsNotEmpty();
        foreach (ItemSlot? slot in actual.Items)
        {
            if (slot is not null)
            {
                await Assert.That(slot.Id > 0).IsTrue();
                await Assert.That(slot.Count > 0).IsTrue();
            }
        }
    }
}
