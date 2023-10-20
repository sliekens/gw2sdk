using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Commerce.Listings;

public class OrderBooks
{
    [Fact]
    public async Task Can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();

        // You wouldn't want to use Take() in production code
        //   but enumerating all entries is too expensive for a test
        // This code will actually try to fetch more than 600 entries
        //  but the extra requests will be cancelled when this test completes
        await foreach (var actual in sut.Commerce.GetOrderBooksBulk(degreeOfParallelism: 3).Take(600))
        {
            Assert.True(actual.Id > 0);
            if (actual.TotalSupply == 0)
            {
                Assert.Null(actual.BestAsk);
                Assert.Empty(actual.Supply);
            }
            else
            {
                Assert.True(actual.BestAsk > Coin.Zero);
                Assert.NotEmpty(actual.Supply);
                Assert.All(
                    actual.Supply,
                    line =>
                    {
                        Assert.True(line.UnitPrice > Coin.Zero);
                        Assert.True(line.Quantity > 0);
                        Assert.True(line.Listings > 0);
                    }
                );
            }

            if (actual.TotalDemand == 0)
            {
                Assert.Null(actual.BestBid);
                Assert.Empty(actual.Demand);
            }
            else
            {
                Assert.True(actual.BestBid > Coin.Zero);
                Assert.NotEmpty(actual.Demand);
                Assert.All(
                    actual.Demand,
                    line =>
                    {
                        Assert.True(line.UnitPrice > Coin.Zero);
                        Assert.True(line.Quantity > 0);
                        Assert.True(line.Listings > 0);
                    }
                );
            }

            if (actual is { TotalDemand: 0 } or { TotalSupply: 0 })
            {
                Assert.Equal(Coin.Zero, actual.BidAskSpread);
            }
            else
            {
                Assert.Equal(actual.BestAsk - actual.BestBid, actual.BidAskSpread);
            }
        }
    }

}
