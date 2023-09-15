﻿using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Commerce.Transactions;

public class Sales
{
    [Fact]
    public async Task Sales_history_can_be_filtered_by_page()
    {
        var accessToken = Composer.Resolve<ApiKey>();
        var sut = Composer.Resolve<Gw2Client>();

        var bids = await sut.Commerce.GetSales(0, 200, accessToken.Key);

        Assert.NotEmpty(bids.Value);
    }
}