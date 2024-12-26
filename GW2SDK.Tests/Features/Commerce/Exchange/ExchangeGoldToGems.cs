using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Commerce.Exchange;

public class ExchangeGoldToGems
{
    [Fact]
    public async Task You_can_exchange_gold_for_gems()
    {
        var sut = Composer.Resolve<Gw2Client>();

        Coin coins = new(100, 0, 0);

        var (actual, context) = await sut.Commerce.ExchangeGoldToGems(coins, cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(context);
        Assert.True(actual.Gems > 0, "100 gold should be worth some gems.");
        Assert.True(actual.ExchangeRate > 0, "Gems can't be free.");
    }
}
