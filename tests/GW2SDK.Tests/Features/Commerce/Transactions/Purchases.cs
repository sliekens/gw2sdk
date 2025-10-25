using GuildWars2.Commerce.Transactions;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Commerce.Transactions;

public class Purchases
{
    [Test]
    public async Task Purchase_history_can_be_filtered_by_page()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        (HashSet<Transaction> purchases, MessageContext context) = await sut.Commerce.GetPurchases(0, 200, accessToken.Key, cancellationToken: TestContext.Current!.CancellationToken);
        // Step through with debugger to see if the values reflect your in-game transactions
        Assert.NotNull(context);
        Assert.NotEmpty(purchases);
    }
}
