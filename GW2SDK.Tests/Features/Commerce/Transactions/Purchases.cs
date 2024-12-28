using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Commerce.Transactions;

public class Purchases
{
    [Fact]
    public async Task Purchase_history_can_be_filtered_by_page()
    {
        var accessToken = TestConfiguration.ApiKey;
        var sut = Composer.Resolve<Gw2Client>();

        var (purchases, context) = await sut.Commerce.GetPurchases(
            0,
            200,
            accessToken.Key,
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Step through with debugger to see if the values reflect your in-game transactions
        Assert.NotNull(context);
        Assert.NotEmpty(purchases);
    }
}
