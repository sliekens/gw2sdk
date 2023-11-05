using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Commerce.Transactions;

public class BuyOrders
{
    [Fact]
    public async Task Current_bids_can_be_filtered_by_page()
    {
        var accessToken = Composer.Resolve<ApiKey>();
        var sut = Composer.Resolve<Gw2Client>();

        var (bids, _) = await sut.Commerce.GetBuyOrders(0, 200, accessToken.Key);

        Assert.NotNull(bids);
    }
}
