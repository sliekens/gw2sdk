using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Commerce.Transactions;

public class SellOrders
{
    [Fact]
    public async Task Current_asks_can_be_filtered_by_page()
    {
        var accessToken = TestConfiguration.ApiKey;
        var sut = Composer.Resolve<Gw2Client>();

        var (offers, context) = await sut.Commerce.GetSellOrders(0, 200, accessToken.Key);

        // Step through with debugger to see if the values reflect your in-game transactions
        Assert.NotNull(context);
        Assert.NotNull(offers);
    }
}
