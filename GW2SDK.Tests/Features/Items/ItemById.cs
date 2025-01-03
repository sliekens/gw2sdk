﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Items;

public class ItemById
{
    [Fact]
    public async Task An_item_can_be_found_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 24;

        var (actual, context) = await sut.Items.GetItemById(
            id,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
