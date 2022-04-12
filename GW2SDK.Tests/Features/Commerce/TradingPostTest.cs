using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GW2SDK.Commerce;
using GW2SDK.Tests.TestInfrastructure;

using Xunit;

namespace GW2SDK.Tests.Features.Commerce;

public class TradingPostTest
{
    [Fact]
    public async Task It_can_calculate_gold_for_gems()
    {
        await using Composer services = new();
        var sut = services.Resolve<TradingPost>();

        Coin coins = new(100, 0, 0);

        var actual = await sut.ExchangeGoldForGems(coins);

        Assert.True(actual.Value.GemsToReceive > 0, "100 gold should be worth some gems.");
        Assert.True(actual.Value.CoinsPerGem > 0, "Gems can't be free.");
    }

    [Fact]
    public async Task It_can_calculate_gems_for_gold()
    {
        await using Composer services = new();
        var sut = services.Resolve<TradingPost>();

        const int gems = 800;

        var actual = await sut.ExchangeGemsForGold(gems);

        Assert.True(actual.Value.CoinsToReceive > 10000, "800 gems should be worth some gold.");
        Assert.True(actual.Value.CoinsPerGem > 0, "Gems can't be free.");
    }

    [Fact]
    public async Task It_can_get_all_item_price_ids()
    {
        await using Composer services = new();
        var sut = services.Resolve<TradingPost>();

        var actual = await sut.GetItemPricesIndex();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task It_can_get_an_item_price_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<TradingPost>();

        const int itemId = 24;

        var actual = await sut.GetItemPriceById(itemId);

        var value = actual.Value;
        Assert.Equal(itemId, value.Id);
        Assert.True(value.TotalSupply > 0);
        Assert.True(value.BestAsk > 0);
        Assert.True(value.TotalDemand > 0);
        Assert.True(value.BestBid > 0);
        Assert.Equal(value.BestAsk - value.BestBid, value.BidAskSpread);
    }

    [Fact]
    public async Task It_can_get_item_prices_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<TradingPost>();

        HashSet<int> ids = new()
        {
            24,
            19699,
            35984
        };

        var actual = await sut.GetItemPricesByIds(ids)
            .ToListAsync();

        Assert.Collection(actual,
            first => Assert.Equal(24, first.Id),
            second => Assert.Equal(19699, second.Id),
            third => Assert.Equal(35984, third.Id));
    }

    [Fact(Skip =
        "This test is best used interactively, otherwise it will hit rate limits in this as well as other tests.")]
    public async Task It_can_get_all_item_prices()
    {
        await using Composer services = new();
        var sut = services.Resolve<TradingPost>();

        await foreach (var actual in sut.GetItemPrices())
        {
            Assert.True(actual.Id > 0);
            if (actual.TotalSupply == 0)
            {
                Assert.True(actual.BestAsk == Coin.Zero);
            }
            else
            {
                Assert.True(actual.BestAsk > Coin.Zero);
            }

            if (actual.TotalDemand == 0)
            {
                Assert.True(actual.BestBid == Coin.Zero);
            }
            else
            {
                Assert.True(actual.BestBid > Coin.Zero);
            }

            if (actual is { TotalDemand: 0 } or { TotalSupply: 0 })
            {
                Assert.Equal(Coin.Zero, actual.BidAskSpread);
            }
            else
            {
                Assert.Equal(actual.BestAsk - actual.BestBid, actual.BidAskSpread);
            }
        }
    }

    [Fact]
    public async Task It_can_get_all_order_book_ids()
    {
        await using Composer services = new();
        var sut = services.Resolve<TradingPost>();

        var actual = await sut.GetOrderBooksIndex();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task It_can_get_an_order_book_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<TradingPost>();

        const int itemId = 24;

        var actual = await sut.GetOrderBookById(itemId);

        var value = actual.Value;
        Assert.Equal(itemId, value.Id);

        Assert.True(value.TotalSupply > 0);
        Assert.True(value.BestAsk > Coin.Zero);
        Assert.True(value.TotalDemand > 0);
        Assert.True(value.BestBid > Coin.Zero);
        Assert.Equal(value.BestAsk - value.BestBid, value.BidAskSpread);

        Assert.Equal(value.TotalDemand, value.Demand.Sum(bid => bid.Quantity));
        Assert.Equal(value.TotalSupply, value.Supply.Sum(ask => ask.Quantity));

        Assert.NotEmpty(value.Supply);
        Assert.All(value.Supply,
            line =>
            {
                Assert.True(line.UnitPrice > Coin.Zero);
                Assert.True(line.Quantity > 0);
                Assert.True(line.Listings > 0);
            });

        Assert.NotEmpty(value.Demand);
        Assert.All(value.Demand,
            line =>
            {
                Assert.True(line.UnitPrice > Coin.Zero);
                Assert.True(line.Quantity > 0);
                Assert.True(line.Listings > 0);
            });
    }

    [Fact]
    public async Task It_can_get_order_books_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<TradingPost>();

        HashSet<int> ids = new()
        {
            24,
            19699,
            35984
        };

        var actual = await sut.GetOrderBooksByIds(ids)
            .ToListAsync();

        Assert.Collection(actual,
            first => Assert.Equal(24, first.Id),
            second => Assert.Equal(19699, second.Id),
            third => Assert.Equal(35984, third.Id));
    }

    [Fact(Skip =
        "This test is best used interactively, otherwise it will hit rate limits in this as well as other tests.")]
    public async Task It_can_get_all_order_books()
    {
        await using Composer services = new();
        var sut = services.Resolve<TradingPost>();

        await foreach (var actual in sut.GetOrderBooks())
        {
            Assert.True(actual.Id > 0);
            if (actual.TotalSupply == 0)
            {
                Assert.Null(actual.BestAsk);
                Assert.Empty(actual.Supply);
            }
            else
            {
                Assert.True(actual.BestAsk > Coin.Zero);
                Assert.NotEmpty(actual.Supply);
                Assert.All(actual.Supply,
                    line =>
                    {
                        Assert.True(line.UnitPrice > Coin.Zero);
                        Assert.True(line.Quantity > 0);
                        Assert.True(line.Listings > 0);
                    });
            }

            if (actual.TotalDemand == 0)
            {
                Assert.Null(actual.BestBid);
                Assert.Empty(actual.Demand);
            }
            else
            {
                Assert.True(actual.BestBid > Coin.Zero);
                Assert.NotEmpty(actual.Demand);
                Assert.All(actual.Demand,
                    line =>
                    {
                        Assert.True(line.UnitPrice > Coin.Zero);
                        Assert.True(line.Quantity > 0);
                        Assert.True(line.Listings > 0);
                    });
            }

            if (actual is { TotalDemand: 0 } or { TotalSupply: 0 })
            {
                Assert.Equal(Coin.Zero, actual.BidAskSpread);
            }
            else
            {
                Assert.Equal(actual.BestAsk - actual.BestBid, actual.BidAskSpread);
            }
        }
    }

    [Fact]
    public async Task It_can_get_your_delivery_box()
    {
        await using Composer service = new();
        var accessToken = service.Resolve<ApiKeyFull>();
        var sut = service.Resolve<TradingPost>();

        var deliveryBox = await sut.GetDeliveryBox(accessToken.Key);

        // Step through with debugger to see if the values reflect your in-game delivery box
        Assert.NotNull(deliveryBox.Value);
    }

    [Fact]
    public async Task It_can_get_your_current_bids()
    {
        await using Composer service = new();
        var accessToken = service.Resolve<ApiKeyFull>();
        var sut = service.Resolve<TradingPost>();

        var bids = await sut.GetBuyOrders(0, 200, accessToken.Key);

        Assert.NotEmpty(bids);
    }

    [Fact]
    public async Task It_can_get_your_current_asks()
    {
        await using Composer service = new();
        var accessToken = service.Resolve<ApiKeyFull>();
        var sut = service.Resolve<TradingPost>();

        var bids = await sut.GetSellOrders(0, 200, accessToken.Key);

        Assert.NotEmpty(bids);
    }

    [Fact]
    public async Task It_can_get_your_purchases()
    {
        await using Composer service = new();
        var accessToken = service.Resolve<ApiKeyFull>();
        var sut = service.Resolve<TradingPost>();

        var bids = await sut.GetPurchases(0, 200, accessToken.Key);

        Assert.NotEmpty(bids);
    }

    [Fact]
    public async Task It_can_get_your_sales()
    {
        await using Composer service = new();
        var accessToken = service.Resolve<ApiKeyFull>();
        var sut = service.Resolve<TradingPost>();

        var bids = await sut.GetSales(0, 200, accessToken.Key);

        Assert.NotEmpty(bids);
    }
}