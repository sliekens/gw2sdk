using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Commerce.Prices;

public class ItemPriceById
{
    
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 24;

        var actual = await sut.Commerce.GetItemPriceById(id);

        var value = actual.Value;
        Assert.Equal(id, value.Id);
        Assert.True(value.TotalSupply > 0);
        Assert.True(value.BestAsk > 0);
        Assert.True(value.TotalDemand > 0);
        Assert.True(value.BestBid > 0);
        Assert.Equal(value.BestAsk - value.BestBid, value.BidAskSpread);
    }
}
