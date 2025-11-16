using GuildWars2.Commerce.Transactions;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Commerce.Transactions;

[ServiceDataSource]
public class SellOrders(Gw2Client sut)
{
    [Test]
    public async Task Current_asks_can_be_filtered_by_page()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;

        (HashSet<Order> offers, MessageContext context) = await sut.Commerce.GetSellOrders(0, 200, accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        // Step through with debugger to see if the values reflect your in-game transactions
        await Assert.That(context).IsNotNull();
        await Assert.That(offers).IsNotNull();
    }
}
