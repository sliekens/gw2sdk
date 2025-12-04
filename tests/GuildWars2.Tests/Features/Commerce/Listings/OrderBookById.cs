using GuildWars2.Commerce.Listings;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Commerce.Listings;

[ServiceDataSource]
public class OrderBookById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int id = 24;
        (OrderBook actual, MessageContext context) = await sut.Commerce.GetOrderBookById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual.Id).IsEqualTo(id);
    }
}
