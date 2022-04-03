using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GW2SDK.Commerce;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Commerce
{
    public class TradingPostTest
    {
        [Fact]
        public async Task It_can_calculate_gold_for_gems()
        {
            await using var services = new Composer();
            var sut = services.Resolve<TradingPost>();

            var coins = new Coin(100, 0, 0);

            var actual = await sut.ExchangeGoldForGems(coins);

            Assert.True(actual.Value.GemsToReceive > 0, "100 gold should be worth some gems.");
            Assert.True(actual.Value.CoinsPerGem > 0, "Gems can't be free.");
        }

        [Fact]
        public async Task It_can_calculate_gems_for_gold()
        {
            await using var services = new Composer();
            var sut = services.Resolve<TradingPost>();

            const int gems = 800;

            var actual = await sut.ExchangeGemsForGold(gems);

            Assert.True(actual.Value.CoinsToReceive > 10000, "800 gems should be worth some gold.");
            Assert.True(actual.Value.CoinsPerGem > 0, "Gems can't be free.");
        }

        [Fact]
        public async Task It_can_get_all_item_price_ids()
        {
            await using var services = new Composer();
            var sut = services.Resolve<TradingPost>();

            var actual = await sut.GetItemPricesIndex();

            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);
        }

        [Fact]
        public async Task It_can_get_an_item_price_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<TradingPost>();

            const int itemId = 24;

            var actual = await sut.GetItemPriceById(itemId);

            Assert.Equal(itemId, actual.Value.Id);
        }

        [Fact]
        public async Task It_can_get_item_prices_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<TradingPost>();

            var ids = new HashSet<int>
            {
                24,
                19699,
                35984
            };

            var actual = await sut.GetItemPricesByIds(ids)
                .ToListAsync();

            Assert.Collection(actual.Values(),
                first => Assert.Equal(24, first.Id),
                second => Assert.Equal(19699, second.Id),
                third => Assert.Equal(35984, third.Id));
        }

        [Fact(Skip =
            "This test is best used interactively, otherwise it will hit rate limits in this as well as other tests.")]
        public async Task It_can_get_all_item_prices()
        {
            await using var services = new Composer();
            var sut = services.Resolve<TradingPost>();

            await foreach (var actual in sut.GetItemPrices())
            {
                Assert.InRange(actual.Value.Id, 1, int.MaxValue);
            }
        }

        [Fact]
        public async Task It_can_get_all_order_book_ids()
        {
            await using var services = new Composer();
            var sut = services.Resolve<TradingPost>();

            var actual = await sut.GetOrderBooksIndex();

            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);
        }

        [Fact]
        public async Task It_can_get_an_order_book_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<TradingPost>();

            const int itemId = 24;

            var actual = await sut.GetOrderBookById(itemId);

            Assert.Equal(itemId, actual.Value.Id);
        }

        [Fact]
        public async Task It_can_get_order_books_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<TradingPost>();

            var ids = new HashSet<int>
            {
                24,
                19699,
                35984
            };

            var actual = await sut.GetOrderBooksByIds(ids)
                .ToListAsync();

            Assert.Collection(actual.Values(),
                first => Assert.Equal(24, first.Id),
                second => Assert.Equal(19699, second.Id),
                third => Assert.Equal(35984, third.Id));
        }

        [Fact(Skip =
            "This test is best used interactively, otherwise it will hit rate limits in this as well as other tests.")]
        public async Task It_can_get_all_order_books()
        {
            await using var services = new Composer();
            var sut = services.Resolve<TradingPost>();

            await foreach (var actual in sut.GetOrderBooks())
            {
                Assert.InRange(actual.Value.Id, 1, int.MaxValue);
            }
        }

        [Fact]
        public async Task It_can_get_your_delivery_box()
        {
            await using var service = new Composer();
            var accessToken = service.Resolve<ApiKeyFull>();
            var sut = service.Resolve<TradingPost>();

            var deliveryBox = await sut.GetDeliveryBox(accessToken.Key);

            Assert.True(deliveryBox.HasValue);
        }

        [Fact]
        public async Task It_can_get_your_current_bids()
        {
            await using var service = new Composer();
            var accessToken = service.Resolve<ApiKeyFull>();
            var sut = service.Resolve<TradingPost>();

            var bids = await sut.GetBuyOrders(0, 200, accessToken.Key);

            Assert.True(bids.HasValues);
        }

        [Fact]
        public async Task It_can_get_your_current_asks()
        {
            await using var service = new Composer();
            var accessToken = service.Resolve<ApiKeyFull>();
            var sut = service.Resolve<TradingPost>();

            var bids = await sut.GetSellOrders(0, 200, accessToken.Key);

            Assert.True(bids.HasValues);
        }

        [Fact]
        public async Task It_can_get_your_purchases()
        {
            await using var service = new Composer();
            var accessToken = service.Resolve<ApiKeyFull>();
            var sut = service.Resolve<TradingPost>();

            var bids = await sut.GetPurchases(0, 200, accessToken.Key);

            Assert.True(bids.HasValues);
        }

        [Fact]
        public async Task It_can_get_your_sales()
        {
            await using var service = new Composer();
            var accessToken = service.Resolve<ApiKeyFull>();
            var sut = service.Resolve<TradingPost>();

            var bids = await sut.GetSales(0, 200, accessToken.Key);

            Assert.True(bids.HasValues);
        }
    }
}
