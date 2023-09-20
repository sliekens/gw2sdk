using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Commerce.Transactions;

public class SellOrders
{
    [Fact]
    public async Task Current_asks_can_be_filtered_by_page()
    {
        var accessToken = Composer.Resolve<ApiKey>();
        var sut = Composer.Resolve<Gw2Client>();

        var bids = await sut.Commerce.GetSellOrders(0, 200, accessToken.Key);

        Assert.NotNull(bids.Value);
    }
}
