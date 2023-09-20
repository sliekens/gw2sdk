using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Commerce.Transactions;

public class Purchases
{
    [Fact]
    public async Task Purchase_history_can_be_filtered_by_page()
    {
        var accessToken = Composer.Resolve<ApiKey>();
        var sut = Composer.Resolve<Gw2Client>();

        var bids = await sut.Commerce.GetPurchases(0, 200, accessToken.Key);

        Assert.NotEmpty(bids.Value);
    }
}
