﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Commerce;

public class CommerceQueryTest
{
    [Fact]
    public async Task Gold_for_gems_exchange_rate_is_available()
    {
        var sut = Composer.Resolve<Gw2Client>();

        Coin coins = new(100, 0, 0);

        var actual = await sut.Commerce.ExchangeGoldForGems(coins);

        Assert.True(actual.Value.GemsToReceive > 0, "100 gold should be worth some gems.");
        Assert.True(actual.Value.CoinsPerGem > 0, "Gems can't be free.");
    }

    [Fact]
    public async Task Gems_for_gold_exchange_rate_is_available()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int gems = 800;

        var actual = await sut.Commerce.ExchangeGemsForGold(gems);

        Assert.True(actual.Value.CoinsToReceive > 10000, "800 gems should be worth some gold.");
        Assert.True(actual.Value.CoinsPerGem > 0, "Gems can't be free.");
    }

    [Fact]
    public async Task Item_prices_index_is_not_empty()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Commerce.GetItemPricesIndex();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }

    [Fact]
    public async Task An_item_price_can_be_found_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int itemId = 24;

        var actual = await sut.Commerce.GetItemPriceById(itemId);

        var value = actual.Value;
        Assert.Equal(itemId, value.Id);
        Assert.True(value.TotalSupply > 0);
        Assert.True(value.BestAsk > 0);
        Assert.True(value.TotalDemand > 0);
        Assert.True(value.BestBid > 0);
        Assert.Equal(value.BestAsk - value.BestBid, value.BidAskSpread);
    }

    [Fact]
    public async Task Item_prices_can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            24,
            19699,
            35984
        };

        var actual = await sut.Commerce.GetItemPricesByIds(ids).ToListAsync();

        Assert.Collection(
            actual,
            first => Assert.Equal(24, first.Id),
            second => Assert.Equal(19699, second.Id),
            third => Assert.Equal(35984, third.Id)
        );
    }

    [Fact(
        Skip =
            "This test is best used interactively, otherwise it will hit rate limits in this as well as other tests."
    )]
    public async Task Item_prices_can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();

        await foreach (var actual in sut.Commerce.GetItemPrices())
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
    public async Task Order_books_index_is_not_empty()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Commerce.GetOrderBooksIndex();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }

    [Fact]
    public async Task An_order_book_can_be_found_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int itemId = 24;

        var actual = await sut.Commerce.GetOrderBookById(itemId);

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
        Assert.All(
            value.Supply,
            line =>
            {
                Assert.True(line.UnitPrice > Coin.Zero);
                Assert.True(line.Quantity > 0);
                Assert.True(line.Listings > 0);
            }
        );

        Assert.NotEmpty(value.Demand);
        Assert.All(
            value.Demand,
            line =>
            {
                Assert.True(line.UnitPrice > Coin.Zero);
                Assert.True(line.Quantity > 0);
                Assert.True(line.Listings > 0);
            }
        );
    }

    [Fact]
    public async Task Order_books_can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            24,
            19699,
            35984
        };

        var actual = await sut.Commerce.GetOrderBooksByIds(ids).ToListAsync();

        Assert.Collection(
            actual,
            first => Assert.Equal(24, first.Id),
            second => Assert.Equal(19699, second.Id),
            third => Assert.Equal(35984, third.Id)
        );
    }

    [Fact(
        Skip =
            "This test is best used interactively, otherwise it will hit rate limits in this as well as other tests."
    )]
    public async Task Order_books_can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();

        await foreach (var actual in sut.Commerce.GetOrderBooks())
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
                Assert.All(
                    actual.Supply,
                    line =>
                    {
                        Assert.True(line.UnitPrice > Coin.Zero);
                        Assert.True(line.Quantity > 0);
                        Assert.True(line.Listings > 0);
                    }
                );
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
                Assert.All(
                    actual.Demand,
                    line =>
                    {
                        Assert.True(line.UnitPrice > Coin.Zero);
                        Assert.True(line.Quantity > 0);
                        Assert.True(line.Listings > 0);
                    }
                );
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
    public async Task The_delivery_box_can_be_found()
    {
        var accessToken = Composer.Resolve<ApiKey>();
        var sut = Composer.Resolve<Gw2Client>();

        var deliveryBox = await sut.Commerce.GetDeliveryBox(accessToken.Key);

        // Step through with debugger to see if the values reflect your in-game delivery box
        Assert.NotNull(deliveryBox.Value);
    }

    [Fact]
    public async Task Current_bids_can_be_filtered_by_page()
    {
        var accessToken = Composer.Resolve<ApiKey>();
        var sut = Composer.Resolve<Gw2Client>();

        var bids = await sut.Commerce.GetBuyOrders(0, 200, accessToken.Key);

        Assert.NotNull(bids.Value);
    }

    [Fact]
    public async Task Current_asks_can_be_filtered_by_page()
    {
        var accessToken = Composer.Resolve<ApiKey>();
        var sut = Composer.Resolve<Gw2Client>();

        var bids = await sut.Commerce.GetSellOrders(0, 200, accessToken.Key);

        Assert.NotNull(bids.Value);
    }

    [Fact]
    public async Task Purchase_history_can_be_filtered_by_page()
    {
        var accessToken = Composer.Resolve<ApiKey>();
        var sut = Composer.Resolve<Gw2Client>();

        var bids = await sut.Commerce.GetPurchases(0, 200, accessToken.Key);

        Assert.NotEmpty(bids.Value);
    }

    [Fact]
    public async Task Sales_history_can_be_filtered_by_page()
    {
        var accessToken = Composer.Resolve<ApiKey>();
        var sut = Composer.Resolve<Gw2Client>();

        var bids = await sut.Commerce.GetSales(0, 200, accessToken.Key);

        Assert.NotEmpty(bids.Value);
    }
}
