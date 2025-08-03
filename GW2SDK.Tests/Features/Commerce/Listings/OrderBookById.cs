using GuildWars2.Commerce.Listings;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Commerce.Listings;

public class OrderBookById
{
    [Fact]
    public async Task Can_be_found()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();

        const int id = 24;

        (OrderBook actual, MessageContext context) = await sut.Commerce.GetOrderBookById(
            id,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
