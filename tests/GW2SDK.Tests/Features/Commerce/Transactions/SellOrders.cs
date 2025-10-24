using GuildWars2.Commerce.Transactions;

using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Commerce.Transactions;

public class SellOrders
{

    [Test]

    public async Task Current_asks_can_be_filtered_by_page()
    {

        ApiKey accessToken = TestConfiguration.ApiKey;

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (HashSet<Order> offers, MessageContext context) = await sut.Commerce.GetSellOrders(0, 200, accessToken.Key, cancellationToken: TestContext.Current!.CancellationToken);
        // Step through with debugger to see if the values reflect your in-game transactions

        Assert.NotNull(context);

        Assert.NotNull(offers);
    }
}
