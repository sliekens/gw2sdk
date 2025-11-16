using GuildWars2.Commerce.Prices;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Commerce.Prices;

[ServiceDataSource]
public class ItemPrices(Gw2Client sut)
{
    [Test]
    public async Task Can_be_enumerated()
    {
        // You wouldn't want to use Take() in production code
        //   but enumerating all entries is too expensive for a test
        // This code will actually try to fetch more than 600 entries
        //  but the extra requests will be cancelled when this test completes
        await foreach ((ItemPrice actual, MessageContext context) in sut.Commerce.GetItemPricesBulk(degreeOfParallelism: 3, cancellationToken: TestContext.Current!.Execution.CancellationToken).Take(600))
        {
            await Assert.That(context).IsNotNull();
            await Assert.That(actual.Id).IsGreaterThan(0);
            if (actual.TotalSupply == 0)
            {
                await Assert.That(actual.BestAsk).IsEqualTo(Coin.Zero);
            }
            else
            {
                await Assert.That(actual.BestAsk).IsGreaterThan(Coin.Zero);
            }

            if (actual.TotalDemand == 0)
            {
                await Assert.That(actual.BestBid).IsEqualTo(Coin.Zero);
            }
            else
            {
                await Assert.That(actual.BestBid).IsGreaterThan(Coin.Zero);
            }

            if (actual is { TotalDemand: 0 } or { TotalSupply: 0 })
            {
                await Assert.That(actual.BidAskSpread).IsEqualTo(Coin.Zero);
            }
            else
            {
                Coin expectedSpread = actual.BestAsk - actual.BestBid;
                await Assert.That(actual.BidAskSpread).IsEqualTo(expectedSpread);
            }
        }
    }
}
