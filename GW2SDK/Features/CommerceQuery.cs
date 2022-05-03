using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Commerce.Delivery;
using GW2SDK.Commerce.Exchange;
using GW2SDK.Commerce.Listings;
using GW2SDK.Commerce.Prices;
using GW2SDK.Commerce.Transactions;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK;

[PublicAPI]
public sealed class CommerceQuery
{
    private readonly HttpClient http;

    public CommerceQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region /v2/commerce/delivery

    [Scope(Permission.TradingPost)]
    public Task<IReplica<DeliveryBox>> GetDeliveryBox(
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

    #region /v2/commerce/prices

    public Task<IReplicaSet<int>> GetItemPricesIndex(CancellationToken cancellationToken = default)
    {
        ItemPricesIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<ItemPrice>> GetItemPriceById(
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
        IProgress<ICollectionContext>? progress = default,
        CancellationToken cancellationToken = default
    )
    {
        var producer = SplitQuery.Create<int, ItemPrice>(
            (range, ct) =>
            {
                ItemPricesByIdsRequest request = new(range)
                {
                    MissingMemberBehavior = missingMemberBehavior
                };
                return request.SendAsync(http, ct);
            },
            progress
        );

        return producer.QueryAsync(itemIds, cancellationToken: cancellationToken);
    }

    public async IAsyncEnumerable<ItemPrice> GetItemPrices(
        MissingMemberBehavior missingMemberBehavior = default,
        IProgress<ICollectionContext>? progress = default,
        [EnumeratorCancellation] CancellationToken cancellationToken = default
    )
    {
        var index = await GetItemPricesIndex(cancellationToken).ConfigureAwait(false);
        var producer = GetItemPricesByIds(
            index.Values,
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

    #region /v2/commerce/listings

    public Task<IReplicaSet<int>> GetOrderBooksIndex(CancellationToken cancellationToken = default)
    {
        OrderBooksIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<OrderBook>> GetOrderBookById(
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
        IProgress<ICollectionContext>? progress = default,
        CancellationToken cancellationToken = default
    )
    {
        var producer = SplitQuery.Create<int, OrderBook>(
            (range, ct) =>
            {
                OrderBooksByIdsRequest request = new(range)
                {
                    MissingMemberBehavior = missingMemberBehavior
                };
                return request.SendAsync(http, ct);
            },
            progress
        );

        return producer.QueryAsync(itemIds, cancellationToken: cancellationToken);
    }

    public async IAsyncEnumerable<OrderBook> GetOrderBooks(
        MissingMemberBehavior missingMemberBehavior = default,
        IProgress<ICollectionContext>? progress = default,
        [EnumeratorCancellation] CancellationToken cancellationToken = default
    )
    {
        var index = await GetOrderBooksIndex(cancellationToken).ConfigureAwait(false);
        var producer = GetOrderBooksByIds(
            index.Values,
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

    #region /v2/commerce/exchange

    public Task<IReplica<GemsForGoldExchange>> ExchangeGemsForGold(
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

    public Task<IReplica<GoldForGemsExchange>> ExchangeGoldForGems(
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

    #region /v2/commerce/transactions

    public Task<IReplicaPage<Order>> GetBuyOrders(
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

    public Task<IReplicaPage<Order>> GetSellOrders(
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

    public Task<IReplicaPage<Transaction>> GetPurchases(
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

    public Task<IReplicaPage<Transaction>> GetSales(
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
