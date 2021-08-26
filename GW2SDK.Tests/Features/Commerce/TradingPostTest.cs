﻿using System.Threading.Tasks;
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

            var ids = new[]
            {
                24,
                19699,
                35984
            };

            var actual = await sut.GetItemPricesByIds(ids);

            Assert.Collection(actual.Values,
                first => Assert.Equal(24, first.Id),
                second => Assert.Equal(19699, second.Id),
                third => Assert.Equal(35984, third.Id));
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

            var ids = new[]
            {
                24,
                19699,
                35984
            };

            var actual = await sut.GetOrderBooksByIds(ids);

            Assert.Collection(actual.Values,
                first => Assert.Equal(24, first.Id),
                second => Assert.Equal(19699, second.Id),
                third => Assert.Equal(35984, third.Id));
        }
    }
}