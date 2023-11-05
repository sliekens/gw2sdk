using System.Runtime.CompilerServices;
using GuildWars2.Commerce.Delivery;
using GuildWars2.Commerce.Exchange;
using GuildWars2.Commerce.Http;
using GuildWars2.Commerce.Listings;
using GuildWars2.Commerce.Prices;
using GuildWars2.Commerce.Transactions;

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

    public Task<(DeliveryBox Value, MessageContext Context)> GetDeliveryBox(
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

    public Task<(HashSet<int> Value, MessageContext Context)> GetItemPricesIndex(
        CancellationToken cancellationToken = default
    )
    {
        ItemPricesIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(ItemPrice Value, MessageContext Context)> GetItemPriceById(
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

    public Task<(HashSet<ItemPrice> Value, MessageContext Context)> GetItemPricesByIds(
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

    public IAsyncEnumerable<(ItemPrice Value, MessageContext Context)> GetItemPricesBulk(
        IReadOnlyCollection<int> itemIds,
        MissingMemberBehavior missingMemberBehavior = default,
        int degreeOfParallelism = BulkQuery.DefaultDegreeOfParallelism,
        int chunkSize = BulkQuery.DefaultChunkSize,
        IProgress<ResultContext>? progress = default,
        CancellationToken cancellationToken = default
    )
    {
        return BulkQuery.QueryAsync(
            itemIds,
            GetChunk,
            degreeOfParallelism,
            chunkSize,
            progress,
            cancellationToken
        );

        // ReSharper disable once VariableHidesOuterVariable (intended, believe it or not)
        async Task<IReadOnlyCollection<(ItemPrice, MessageContext)>> GetChunk(
            IReadOnlyCollection<int> chunk,
            CancellationToken cancellationToken
        )
        {
            var (values, context) =
                await GetItemPricesByIds(chunk, missingMemberBehavior, cancellationToken)
                    .ConfigureAwait(false);
            return values.Select(value => (value, context)).ToList();
        }
    }

    public async IAsyncEnumerable<(ItemPrice Value, MessageContext Context)> GetItemPricesBulk(
        MissingMemberBehavior missingMemberBehavior = default,
        int degreeOfParallelism = BulkQuery.DefaultDegreeOfParallelism,
        int chunkSize = BulkQuery.DefaultChunkSize,
        IProgress<ResultContext>? progress = default,
        [EnumeratorCancellation] CancellationToken cancellationToken = default
    )
    {
        var (value, _) = await GetItemPricesIndex(cancellationToken).ConfigureAwait(false);
        var producer = GetItemPricesBulk(
            value,
            missingMemberBehavior,
            degreeOfParallelism,
            chunkSize,
            progress,
            cancellationToken
        );
        await foreach (var itemPrice in producer.ConfigureAwait(false))
        {
            yield return itemPrice;
        }
    }

    #endregion

    #region v2/commerce/listings

    public Task<(HashSet<int> Value, MessageContext Context)> GetOrderBooksIndex(
        CancellationToken cancellationToken = default
    )
    {
        OrderBooksIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(OrderBook Value, MessageContext Context)> GetOrderBookById(
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

    public Task<(HashSet<OrderBook> Value, MessageContext Context)> GetOrderBooksByIds(
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

    public IAsyncEnumerable<(OrderBook Value, MessageContext Context)> GetOrderBooksBulk(
        IReadOnlyCollection<int> itemIds,
        MissingMemberBehavior missingMemberBehavior = default,
        int degreeOfParallelism = BulkQuery.DefaultDegreeOfParallelism,
        int chunkSize = BulkQuery.DefaultChunkSize,
        IProgress<ResultContext>? progress = default,
        CancellationToken cancellationToken = default
    )
    {
        return BulkQuery.QueryAsync(
            itemIds,
            GetChunk,
            degreeOfParallelism,
            chunkSize,
            progress,
            cancellationToken
        );

        // ReSharper disable once VariableHidesOuterVariable (intended, believe it or not)
        async Task<IReadOnlyCollection<(OrderBook, MessageContext)>> GetChunk(
            IReadOnlyCollection<int> chunk,
            CancellationToken cancellationToken
        )
        {
            var (values, context) =
                await GetOrderBooksByIds(chunk, missingMemberBehavior, cancellationToken)
                    .ConfigureAwait(false);
            return values.Select(value => (value, context)).ToList();
        }
    }

    public async IAsyncEnumerable<(OrderBook Value, MessageContext Context)> GetOrderBooksBulk(
        MissingMemberBehavior missingMemberBehavior = default,
        int degreeOfParallelism = BulkQuery.DefaultDegreeOfParallelism,
        int chunkSize = BulkQuery.DefaultChunkSize,
        IProgress<ResultContext>? progress = default,
        [EnumeratorCancellation] CancellationToken cancellationToken = default
    )
    {
        var (value, _) = await GetOrderBooksIndex(cancellationToken).ConfigureAwait(false);
        var producer = GetOrderBooksBulk(
            value,
            missingMemberBehavior,
            degreeOfParallelism,
            chunkSize,
            progress,
            cancellationToken
        );
        await foreach (var orderBook in producer.ConfigureAwait(false))
        {
            yield return orderBook;
        }
    }

    #endregion

    #region v2/commerce/exchange

    public Task<(GemsForGoldExchange Value, MessageContext Context)> ExchangeGemsForGold(
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

    public Task<(GoldForGemsExchange Value, MessageContext Context)> ExchangeGoldForGems(
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

    public Task<(HashSet<Order> Value, MessageContext Context)> GetBuyOrders(
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

    public Task<(HashSet<Order> Value, MessageContext Context)> GetSellOrders(
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

    public Task<(HashSet<Transaction> Value, MessageContext Context)> GetPurchases(
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

    public Task<(HashSet<Transaction> Value, MessageContext Context)> GetSales(
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
