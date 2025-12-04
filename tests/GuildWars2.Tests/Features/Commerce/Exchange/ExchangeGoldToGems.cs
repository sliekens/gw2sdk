using GuildWars2.Commerce.Exchange;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Commerce.Exchange;

[ServiceDataSource]
public class ExchangeGoldToGems(Gw2Client sut)
{
    [Test]
    public async Task You_can_exchange_gold_for_gems()
    {
        Coin coins = new(100, 0, 0);
        (GoldToGems actual, MessageContext context) = await sut.Commerce.ExchangeGoldToGems(coins, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual.Gems).IsGreaterThan(0).Because("100 gold should be worth some gems.");
        await Assert.That(actual.ExchangeRate).IsGreaterThan(0).Because("Gems can't be free.");
    }
}
