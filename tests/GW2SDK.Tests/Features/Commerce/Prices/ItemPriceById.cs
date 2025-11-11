using GuildWars2.Commerce.Prices;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Commerce.Prices;

[ServiceDataSource]
public class ItemPriceById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int id = 24;
        (ItemPrice actual, MessageContext context) = await sut.Commerce.GetItemPriceById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
