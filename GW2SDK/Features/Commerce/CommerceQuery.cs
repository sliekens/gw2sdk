using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Annotations;
using GuildWars2.Commerce.Delivery;
using GuildWars2.Commerce.Exchange;
using GuildWars2.Commerce.Listings;
using GuildWars2.Commerce.Prices;
using GuildWars2.Commerce.Transactions;
using JetBrains.Annotations;

namespace GuildWars2.Commerce;

[PublicAPI]
public sealed class CommerceQuery
{
    private readonly HttpClient http;

    public CommerceQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/commerce/delivery

    [Scope(Permission.TradingPost)]
    public Task<Replica<DeliveryBox>> GetDeliveryBox(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        DeliveryRequest request = new()
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/commerce/prices

    public Task<Replica<HashSet<int>>> GetItemPricesIndex(
        CancellationToken cancellationToken = default
    )
    {
        ItemPricesIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<ItemPrice>> GetItemPriceById(
        int itemId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ItemPriceByIdRequest request = new(itemId)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public IAsyncEnumerable<ItemPrice> GetItemPricesByIds(
        IReadOnlyCollection<int> itemIds,
        MissingMemberBehavior missingMemberBehavior = default,
        IProgress<ResultContext>? progress = default,
        CancellationToken cancellationToken = default
    )
    {
        var producer = SplitQuery.Create<int, ItemPrice>(
            async (range, ct) =>
            {
                ItemPricesByIdsRequest request = new(range)
                {
                    MissingMemberBehavior = missingMemberBehavior
                };
                var response = await request.SendAsync(http, ct).ConfigureAwait(false);
                return response.Value;
            }
        );

        return producer.QueryAsync(
            itemIds,
            progress: progress,
            cancellationToken: cancellationToken
        );
    }

    public async IAsyncEnumerable<ItemPrice> GetItemPrices(
        MissingMemberBehavior missingMemberBehavior = default,
        IProgress<ResultContext>? progress = default,
        [EnumeratorCancellation] CancellationToken cancellationToken = default
    )
    {
        var index = await GetItemPricesIndex(cancellationToken).ConfigureAwait(false);
        var producer = GetItemPricesByIds(
            index.Value,
            missingMemberBehavior,
            progress,
            cancellationToken
        );
        await foreach (var itemPrice in producer.WithCancellation(cancellationToken)
            .ConfigureAwait(false))
        {
            yield return itemPrice;
        }
    }

    #endregion

    #region v2/commerce/listings

    public Task<Replica<HashSet<int>>> GetOrderBooksIndex(
        CancellationToken cancellationToken = default
    )
    {
        OrderBooksIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<OrderBook>> GetOrderBookById(
        int itemId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        OrderBookByIdRequest request = new(itemId)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public IAsyncEnumerable<OrderBook> GetOrderBooksByIds(
        IReadOnlyCollection<int> itemIds,
        MissingMemberBehavior missingMemberBehavior = default,
        IProgress<ResultContext>? progress = default,
        CancellationToken cancellationToken = default
    )
    {
        var producer = SplitQuery.Create<int, OrderBook>(
            async (range, ct) =>
            {
                OrderBooksByIdsRequest request = new(range)
                {
                    MissingMemberBehavior = missingMemberBehavior
                };
                var response = await request.SendAsync(http, ct).ConfigureAwait(false);
                return response.Value;
            }
        );

        return producer.QueryAsync(
            itemIds,
            progress: progress,
            cancellationToken: cancellationToken
        );
    }

    public async IAsyncEnumerable<OrderBook> GetOrderBooks(
        MissingMemberBehavior missingMemberBehavior = default,
        IProgress<ResultContext>? progress = default,
        [EnumeratorCancellation] CancellationToken cancellationToken = default
    )
    {
        var index = await GetOrderBooksIndex(cancellationToken).ConfigureAwait(false);
        var producer = GetOrderBooksByIds(
            index.Value,
            missingMemberBehavior,
            progress,
            cancellationToken
        );
        await foreach (var orderBook in producer.WithCancellation(cancellationToken)
            .ConfigureAwait(false))
        {
            yield return orderBook;
        }
    }

    #endregion

    #region v2/commerce/exchange

    public Task<Replica<GemsForGoldExchange>> ExchangeGemsForGold(
        int gemsCount,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ExchangeGemsForGoldRequest request = new(gemsCount)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<GoldForGemsExchange>> ExchangeGoldForGems(
        Coin coinsCount,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ExchangeGoldForGemsRequest request = new(coinsCount)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/commerce/transactions

    public Task<Replica<HashSet<Order>>> GetBuyOrders(
        int pageIndex,
        int? pageSize,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        BuyOrdersRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Order>>> GetSellOrders(
        int pageIndex,
        int? pageSize,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SellOrdersRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Transaction>>> GetPurchases(
        int pageIndex,
        int? pageSize,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        PurchasesRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Transaction>>> GetSales(
        int pageIndex,
        int? pageSize,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SalesRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion
}
