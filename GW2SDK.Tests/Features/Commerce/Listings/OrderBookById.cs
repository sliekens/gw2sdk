using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Commerce.Listings;

public class OrderBookById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 24;

        var (actual, _) = await sut.Commerce.GetOrderBookById(id);

        Assert.Equal(id, actual.Id);

        Assert.True(actual.TotalSupply > 0);
        Assert.True(actual.BestAsk > Coin.Zero);
        Assert.True(actual.TotalDemand > 0);
        Assert.True(actual.BestBid > Coin.Zero);
        Assert.Equal(actual.BestAsk - actual.BestBid, actual.BidAskSpread);

        Assert.Equal(actual.TotalDemand, actual.Demand.Sum(bid => bid.Quantity));
        Assert.Equal(actual.TotalSupply, actual.Supply.Sum(ask => ask.Quantity));

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
}
