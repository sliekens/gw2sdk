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
        Assert.NotNull(actual);
        Assert.NotEmpty(actual.Bags);
        Assert.All(actual.Bags, bag =>
        {
            if (bag is not null)
            {
                Assert.True(bag.Id > 0);
                Assert.True(bag.Size > 0);
                Assert.Equal(bag.Size, bag.Inventory.Items.Count);
                Assert.All(bag.Inventory.Items, slot =>
                {
                    if (slot is not null)
                    {
                        Assert.True(slot.Id > 0);
                        Assert.True(slot.Count > 0);
                    }
                });
            }
        });
    }
}
