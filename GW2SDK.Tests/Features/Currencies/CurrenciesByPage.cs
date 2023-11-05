﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Currencies;

public class CurrenciesByPage
{
    [Fact]
    public async Task Currencies_can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Wallet.GetCurrenciesByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.NotNull(context.PageContext);
        Assert.Equal(3, context.PageContext.PageSize);
    }
}
