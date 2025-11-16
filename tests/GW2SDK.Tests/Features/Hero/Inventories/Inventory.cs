using GuildWars2.Hero.Inventories;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Inventories;

[ServiceDataSource]
public class Inventory(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found_by_character_name()
    {
        TestCharacter character = TestConfiguration.TestCharacter;
        ApiKey accessToken = TestConfiguration.ApiKey;
        (Baggage actual, _) = await sut.Hero.Inventory.GetInventory(character.Name, accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotNull();
        await Assert.That(actual.Bags).IsNotEmpty();
        foreach (Bag? bag in actual.Bags)
        {
            if (bag is not null)
            {
                await Assert.That(bag.Id > 0).IsTrue();
                await Assert.That(bag.Size > 0).IsTrue();
                await Assert.That(bag.Inventory.Items.Count).IsEqualTo(bag.Size);
                foreach (ItemSlot? slot in bag.Inventory.Items)
                {
                    if (slot is not null)
                    {
                        await Assert.That(slot.Id > 0).IsTrue();
                        await Assert.That(slot.Count > 0).IsTrue();
                    }
                }
            }
        }
    }
}
