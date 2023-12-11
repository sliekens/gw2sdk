using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Commerce.Prices;

public class ItemPriceById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 24;

        var (actual, _) = await sut.Commerce.GetItemPriceById(id);

        Assert.Equal(id, actual.Id);
        Assert.True(actual.TotalSupply > 0);
        Assert.True(actual.BestAsk > 0);
        Assert.True(actual.TotalDemand > 0);
        Assert.True(actual.BestBid > 0);
        Assert.Equal(actual.BestAsk - actual.BestBid, actual.BidAskSpread);
    }
}
