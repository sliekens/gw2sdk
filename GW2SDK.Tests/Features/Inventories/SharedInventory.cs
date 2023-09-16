﻿using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Inventories;

public class SharedInventory
{
    [Fact]
    public async Task Can_be_listed()
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
