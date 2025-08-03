using GuildWars2.Commerce.Transactions;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Commerce.Transactions;

public class BuyOrders
{
    [Fact]
    public async Task Current_bids_can_be_filtered_by_page()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        var sut = Composer.Resolve<Gw2Client>();

        (HashSet<Order> bids, MessageContext context) = await sut.Commerce.GetBuyOrders(
            0,
            200,
            accessToken.Key,
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Step through with debugger to see if the values reflect your in-game transactions
        Assert.NotNull(context);
        Assert.NotNull(bids);
    }
}
