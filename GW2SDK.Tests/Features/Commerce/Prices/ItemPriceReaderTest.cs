using System.Text.Json;
using GuildWars2.Commerce.Prices;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Commerce.Prices;

public class ItemPriceReaderTest : IClassFixture<ItemPriceFixture>
{
    public ItemPriceReaderTest(ItemPriceFixture fixture)
    {
        this.fixture = fixture;
    }

    private readonly ItemPriceFixture fixture;

    private static class ItemPriceFact
    {
        public static void Id_is_positive(ItemPrice actual) =>
            Assert.InRange(actual.Id, 1, int.MaxValue);

        public static void Best_ask_is_greater_than_best_bid(ItemPrice actual)
        {
            if (actual.TotalDemand > 0 && actual.TotalSupply > 0)
            {
                Assert.True(actual.BestAsk - actual.BestBid > 0);
            }
        }
    }

    [Fact]
    public void Item_prices_can_be_created_from_json()
    {
        foreach (var json in fixture.ItemPrices)
        {
            using var document = JsonDocument.Parse(json);

            var actual = document.RootElement.GetItemPrice(MissingMemberBehavior.Error);

            ItemPriceFact.Id_is_positive(actual);
            ItemPriceFact.Best_ask_is_greater_than_best_bid(actual);
        }
    }
}
