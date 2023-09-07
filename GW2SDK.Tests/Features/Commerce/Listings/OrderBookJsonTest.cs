using System.Text.Json;
using GuildWars2.Commerce.Listings;
using Xunit;

namespace GuildWars2.Tests.Features.Commerce.Listings;
public class OrderBookJsonTest : IClassFixture<OrderBookFixture>
{
    public OrderBookJsonTest(OrderBookFixture fixture)
    {
        this.fixture = fixture;
    }

    private readonly OrderBookFixture fixture;

    [Fact]
    public void Order_book_can_be_created_from_json()
    {
        foreach (var json in fixture.ItemPrices)
        {
            using var document = JsonDocument.Parse(json);

            var actual = document.RootElement.GetOrderBook(MissingMemberBehavior.Error);

            actual.Id_is_positive();
        }
    }
}
