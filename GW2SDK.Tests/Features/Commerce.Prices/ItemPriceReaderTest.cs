using System.Text.Json;
using GW2SDK.Commerce.Prices;
using GW2SDK.Json;
using GW2SDK.Tests.Features.Commerce.Prices.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Commerce.Prices
{
    public class ItemPriceReaderTest : IClassFixture<ItemPriceFixture>
    {
        public ItemPriceReaderTest(ItemPriceFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly ItemPriceFixture _fixture;

        private static class ItemPriceFact
        {
            public static void Id_is_positive(ItemPrice actual) => Assert.InRange(actual.Id, 1, int.MaxValue);

            public static void Best_ask_is_greater_than_best_bid(ItemPrice actual)
            {
                if (actual.TotalDemand > 0 && actual.TotalSupply > 0)
                {
                    Assert.True(actual.BestAsk - actual.BestBid > 0);
                }
            }
        }

        [Fact]
        [Trait("Feature", "Commerce.Prices")]
        [Trait("Category", "Integration")]
        [Trait("Importance", "Critical")]
        public void Item_prices_can_be_created_from_json()
        {
            var sut = new ItemPriceReader();

            AssertEx.ForEach(_fixture.ItemPrices,
                json =>
                {
                    using var document = JsonDocument.Parse(json);

                    var actual = sut.Read(document.RootElement, MissingMemberBehavior.Error);

                    ItemPriceFact.Id_is_positive(actual);
                    ItemPriceFact.Best_ask_is_greater_than_best_bid(actual);
                });
        }
    }
}
