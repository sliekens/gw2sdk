using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Commerce.Listings;

public class OrderBookById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 24;

        var (actual, context) = await sut.Commerce.GetOrderBookById(id);

        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
