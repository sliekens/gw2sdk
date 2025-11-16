using GuildWars2.Commerce.Listings;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Commerce.Listings;

[ServiceDataSource]
public class OrderBooks(Gw2Client sut)
{
    [Test]
    public async Task Can_be_enumerated()
    {
        // You wouldn't want to use Take() in production code
        //   but enumerating all entries is too expensive for a test
        // This code will actually try to fetch more than 600 entries
        //  but the extra requests will be cancelled when this test completes
        await foreach ((OrderBook actual, MessageContext context) in sut.Commerce.GetOrderBooksBulk(degreeOfParallelism: 3, cancellationToken: TestContext.Current!.Execution.CancellationToken).Take(600))
        {
            await Assert.That(context).IsNotNull();
            await Assert.That(actual.Id).IsGreaterThan(0);
            if (actual.TotalSupply == 0)
            {
                await Assert.That(actual.BestAsk).IsNull();
                await Assert.That(actual.Supply).IsEmpty();
            }
            else
            {
                await Assert.That(actual.BestAsk).IsNotNull().And.IsGreaterThan(Coin.Zero);
                await Assert.That(actual.Supply).IsNotEmpty();
                using (Assert.Multiple())
                {
                    foreach (OrderBookLine line in actual.Supply)
                    {
                        await Assert.That(line)
                            .Member(l => l.UnitPrice, unitPrice => unitPrice.IsGreaterThan(Coin.Zero))
                            .And.Member(l => l.Quantity, quantity => quantity.IsGreaterThan(0))
                            .And.Member(l => l.Listings, listings => listings.IsGreaterThan(0));
                    }
                }
            }

            if (actual.TotalDemand == 0)
            {
                await Assert.That(actual.BestBid).IsNull();
                await Assert.That(actual.Demand).IsEmpty();
            }
            else
            {
                await Assert.That(actual.BestBid).IsNotNull().And.IsGreaterThan(Coin.Zero);
                await Assert.That(actual.Demand).IsNotEmpty();
                using (Assert.Multiple())
                {
                    foreach (OrderBookLine line in actual.Demand)
                    {
                        await Assert.That(line)
                            .Member(l => l.UnitPrice, unitPrice => unitPrice.IsGreaterThan(Coin.Zero))
                            .And.Member(l => l.Quantity, quantity => quantity.IsGreaterThan(0))
                            .And.Member(l => l.Listings, listings => listings.IsGreaterThan(0));
                    }
                }
            }

            if (actual is { TotalDemand: 0 } or { TotalSupply: 0 })
            {
                await Assert.That(actual.BidAskSpread).IsEqualTo(Coin.Zero);
            }
            else
            {
                Coin expectedSpread = actual.BestAsk!.Value - actual.BestBid!.Value;
                await Assert.That(actual.BidAskSpread).IsEqualTo(expectedSpread);
            }
        }
    }
}
