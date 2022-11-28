using System.Text.Json;
using GuildWars2.Commerce.Listings;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Commerce.Listings;

public class OrderBookReaderTest : IClassFixture<OrderBookFixture>
{
    public OrderBookReaderTest(OrderBookFixture fixture)
    {
        this.fixture = fixture;
    }

    private readonly OrderBookFixture fixture;

    private static class OrderBookFact
    {
        public static void Id_is_positive(OrderBook actual) =>
            Assert.InRange(actual.Id, 1, int.MaxValue);
    }

    [Fact]
    public void Order_book_can_be_created_from_json() =>
        AssertEx.ForEach(
            fixture.ItemPrices,
            json =>
            {
                using var document = JsonDocument.Parse(json);

                var actual = document.RootElement.GetOrderBook(MissingMemberBehavior.Error);

                OrderBookFact.Id_is_positive(actual);
            }
        );
}
