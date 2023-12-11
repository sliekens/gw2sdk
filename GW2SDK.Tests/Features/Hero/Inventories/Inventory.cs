using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Inventories;

public class Inventory
{
    [Fact]
    public async Task Can_be_found_by_character_name()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = Composer.Resolve<TestCharacter>();
        var accessToken = Composer.Resolve<ApiKey>();

        var (actual, _) = await sut.Hero.Inventory.GetInventory(character.Name, accessToken.Key);

        Assert.NotNull(actual);
        Assert.NotEmpty(actual.Bags);
        Assert.All(
            actual.Bags,
            bag =>
            {
                if (bag is not null)
                {
                    Assert.True(bag.Id > 0);
                    Assert.True(bag.Size > 0);
                    Assert.Equal(bag.Size, bag.Inventory.Items.Count);
                    Assert.All(
                        bag.Inventory.Items,
                        slot =>
                        {
                            if (slot is not null)
                            {
                                Assert.True(slot.Id > 0);
                                Assert.True(slot.Count > 0);
                            }
                        }
                    );
                }
            }
        );
    }
}
