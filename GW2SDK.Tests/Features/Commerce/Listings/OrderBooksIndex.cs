using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Commerce.Listings;

public class OrderBooksIndex
{
    [Fact]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (HashSet<int> actual, MessageContext context) =
            await sut.Commerce.GetOrderBooksIndex(TestContext.Current.CancellationToken);

        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
    }
}
