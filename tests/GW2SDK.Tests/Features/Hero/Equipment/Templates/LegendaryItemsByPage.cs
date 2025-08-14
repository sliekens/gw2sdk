﻿using GuildWars2.Hero.Equipment.Templates;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Templates;

public class LegendaryItemsByPage
{
    [Fact]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();

        const int pageSize = 3;
        (HashSet<LegendaryItem> actual, MessageContext context) = await sut.Hero.Equipment.Templates.GetLegendaryItemsByPage(
            0,
            pageSize,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotNull(context.Links);
        Assert.Equal(pageSize, context.PageSize);
        Assert.Equal(pageSize, context.ResultCount);
        Assert.True(context.PageTotal > 0);
        Assert.True(context.ResultTotal > 0);
        Assert.Equal(pageSize, actual.Count);
        Assert.All(actual, Assert.NotNull);
    }
}
