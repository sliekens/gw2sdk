using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Inventories;

public class Inventory
{
    [Fact]
    public async Task Can_be_found_by_character_name()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = Composer.Resolve<TestCharacter>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Inventory.GetInventory(character.Name, accessToken.Key);

        Assert.NotNull(actual.Value);
        Assert.NotEmpty(actual.Value.Bags);
        Assert.All(
            actual.Value.Bags,
            bag =>
            {
                if (bag is not null)
                {
                    Assert.True(bag.Id > 0);
                    Assert.True(bag.Size > 0);
                    Assert.Equal(bag.Size, bag.Inventory.Count);
                    Assert.All(
                        bag.Inventory,
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
