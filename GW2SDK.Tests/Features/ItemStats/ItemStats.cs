﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.ItemStats;

public class ItemStats
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.ItemStats.GetItemStats();

        Assert.NotNull(context.ResultContext);
        Assert.Equal(context.ResultContext.ResultTotal, actual.Count);
    }
}
