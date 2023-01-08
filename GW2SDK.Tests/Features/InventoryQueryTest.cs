﻿using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features;

public class InventoryQueryTest
{
    [Fact]
    public async Task Inventory_can_be_found_by_character_name()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = Composer.Resolve<TestCharacter>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Inventory.GetInventory(character.Name, accessToken.Key);

        Assert.NotNull(actual.Value);
        Assert.NotEmpty(actual.Value.Bags);
        Assert.All(
            actual.Value.Bags,
            bags =>
            {
                if (bags is not null)
                {
                    Assert.True(bags.Id > 0);
                    Assert.True(bags.Size > 0);
                    Assert.All(
                        bags.Inventory,
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

    [Fact]
    public async Task Shared_inventory_can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Inventory.GetSharedInventory(accessToken.Key);

        Assert.NotEmpty(actual.Value);
        Assert.All(
            actual.Value,
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
