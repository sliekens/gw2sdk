using System.Text.Json;
using GW2SDK.Commerce.Listings;
using GW2SDK.Json;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Commerce.Listings
{
    public class OrderBookReaderTest : IClassFixture<OrderBookFixture>
    {
        public OrderBookReaderTest(OrderBookFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly OrderBookFixture _fixture;

        private static class OrderBookFact
        {
            public static void Id_is_positive(OrderBook actual) => Assert.InRange(actual.Id, 1, int.MaxValue);
        }

        [Fact]
        [Trait("Feature", "Commerce.Listings")]
        [Trait("Category", "Integration")]
        public void Order_book_can_be_created_from_json()
        {
            var sut = new OrderBookReader();

            AssertEx.ForEach(_fixture.ItemPrices,
                json =>
                {
                    using var document = JsonDocument.Parse(json);

                    var actual = sut.Read(document.RootElement, MissingMemberBehavior.Error);

                    OrderBookFact.Id_is_positive(actual);
                });
        }
    }
}
