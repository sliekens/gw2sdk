using System.Text.Json;
using GuildWars2.Commerce.Prices;
using Xunit;

namespace GuildWars2.Tests.Features.Commerce.Prices;

public class ItemPriceJsonTest : IClassFixture<ItemPriceFixture>
{
    public ItemPriceJsonTest(ItemPriceFixture fixture)
    {
        this.fixture = fixture;
    }

    private readonly ItemPriceFixture fixture;

    [Fact]
    public void Item_prices_can_be_created_from_json()
    {
        foreach (var json in fixture.ItemPrices)
        {
            using var document = JsonDocument.Parse(json);

            var actual = document.RootElement.GetItemPrice(MissingMemberBehavior.Error);

            actual.Id_is_positive();
            actual.Best_ask_is_greater_than_best_bid();
        }
    }
}
