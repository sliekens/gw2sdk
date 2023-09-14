using System.Linq;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Commerce.Listings;

public class OrderBookById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 24;

        var actual = await sut.Commerce.GetOrderBookById(id);

        var value = actual.Value;
        Assert.Equal(id, value.Id);

        Assert.True(value.TotalSupply > 0);
        Assert.True(value.BestAsk > Coin.Zero);
        Assert.True(value.TotalDemand > 0);
        Assert.True(value.BestBid > Coin.Zero);
        Assert.Equal(value.BestAsk - value.BestBid, value.BidAskSpread);

        Assert.Equal(value.TotalDemand, value.Demand.Sum(bid => bid.Quantity));
        Assert.Equal(value.TotalSupply, value.Supply.Sum(ask => ask.Quantity));

        Assert.NotEmpty(value.Supply);
        Assert.All(
            value.Supply,
            line =>
            {
                Assert.True(line.UnitPrice > Coin.Zero);
                Assert.True(line.Quantity > 0);
                Assert.True(line.Listings > 0);
            }
        );

        Assert.NotEmpty(value.Demand);
        Assert.All(
            value.Demand,
            line =>
            {
                Assert.True(line.UnitPrice > Coin.Zero);
                Assert.True(line.Quantity > 0);
                Assert.True(line.Listings > 0);
            }
        );
    }
}
