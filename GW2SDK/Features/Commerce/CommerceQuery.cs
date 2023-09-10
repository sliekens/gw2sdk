﻿using System;
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

    public Task<Replica<HashSet<ItemPrice>>> GetItemPricesByIds(
        IReadOnlyCollection<int> itemIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ItemPricesByIdsRequest request = new(itemIds)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public IAsyncEnumerable<ItemPrice> GetItemPricesBulk(
        IReadOnlyCollection<int> itemIds,
        MissingMemberBehavior missingMemberBehavior = default,
        int degreeOfParalllelism = BulkQuery.DefaultDegreeOfParalllelism,
        int chunkSize = BulkQuery.DefaultChunkSize,
        IProgress<ResultContext>? progress = default,
        CancellationToken cancellationToken = default
    )
    {
        var producer = BulkQuery.Create<int, ItemPrice>(
            async (chunk, ct) =>
            {
                var response = await GetItemPricesByIds(chunk, missingMemberBehavior, ct)
                    .ConfigureAwait(false);
                return response.Value;
            }
        );

        return producer.QueryAsync(
            itemIds,
            degreeOfParalllelism,
            chunkSize,
            progress: progress,
            cancellationToken: cancellationToken
        );
    }

    public async IAsyncEnumerable<ItemPrice> GetItemPricesBulk(
        MissingMemberBehavior missingMemberBehavior = default,
        int degreeOfParalllelism = BulkQuery.DefaultDegreeOfParalllelism,
        int chunkSize = BulkQuery.DefaultChunkSize,
        IProgress<ResultContext>? progress = default,
        [EnumeratorCancellation] CancellationToken cancellationToken = default
    )
    {
        var index = await GetItemPricesIndex(cancellationToken).ConfigureAwait(false);
        var producer = GetItemPricesBulk(
            index.Value,
            missingMemberBehavior,
            degreeOfParalllelism,
            chunkSize,
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

    public Task<Replica<HashSet<OrderBook>>> GetOrderBooksByIds(
        IReadOnlyCollection<int> itemIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        OrderBooksByIdsRequest request = new(itemIds)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public IAsyncEnumerable<OrderBook> GetOrderBooksBulk(
        IReadOnlyCollection<int> itemIds,
        MissingMemberBehavior missingMemberBehavior = default,
        int degreeOfParalllelism = BulkQuery.DefaultDegreeOfParalllelism,
        int chunkSize = BulkQuery.DefaultChunkSize,
        IProgress<ResultContext>? progress = default,
        CancellationToken cancellationToken = default
    )
    {
        var producer = BulkQuery.Create<int, OrderBook>(
            async (chunk, ct) =>
            {
                var response = await GetOrderBooksByIds(chunk, missingMemberBehavior, ct)
                    .ConfigureAwait(false);
                return response.Value;
            }
        );

        return producer.QueryAsync(
            itemIds,
            degreeOfParalllelism,
            chunkSize,
            progress: progress,
            cancellationToken: cancellationToken
        );
    }

    public async IAsyncEnumerable<OrderBook> GetOrderBooksBulk(
        MissingMemberBehavior missingMemberBehavior = default,
        int degreeOfParalllelism = BulkQuery.DefaultDegreeOfParalllelism,
        int chunkSize = BulkQuery.DefaultChunkSize,
        IProgress<ResultContext>? progress = default,
        [EnumeratorCancellation] CancellationToken cancellationToken = default
    )
    {
        var index = await GetOrderBooksIndex(cancellationToken).ConfigureAwait(false);
        var producer = GetOrderBooksBulk(
            index.Value,
            missingMemberBehavior,
            degreeOfParalllelism,
            chunkSize,
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
