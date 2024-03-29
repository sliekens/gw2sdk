﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Items;

public class ItemsIndex
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Items.GetItemsIndex();

        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
    }
}
