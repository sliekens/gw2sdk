﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Banking;

public class MaterialCategoriesByPage
{
    [Fact]
    public async Task Can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int pageSize = 3;
        var (actual, context) = await sut.Hero.Bank.GetMaterialCategoriesByPage(0, pageSize);

        Assert.Equal(pageSize, actual.Count);
        Assert.NotNull(context.Links);
        Assert.Equal(pageSize, context.PageSize);
        Assert.Equal(context.ResultCount, pageSize);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_id();
                entry.Has_name();
                entry.Has_items();
                entry.Has_order();
            }
        );
    }
}
