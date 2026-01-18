using System.Runtime.CompilerServices;
using System.Text.Json;

using GuildWars2.Commerce.Delivery;
using GuildWars2.Commerce.Exchange;
using GuildWars2.Commerce.Listings;
using GuildWars2.Commerce.Prices;
using GuildWars2.Commerce.Transactions;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Commerce;

/// <summary>Provides query methods for Black Lion Trading Company services.</summary>
public sealed class CommerceClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="CommerceClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public CommerceClient(HttpClient httpClient)
    {
        ThrowHelper.ThrowIfNull(httpClient);
        this.httpClient = httpClient;
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/commerce/delivery

    /// <summary>Retrieves information about items and coins ready for pickup from the Black Lion Trading Company. This
    /// endpoint is only accessible with a valid access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(DeliveryBox Value, MessageContext Context)> GetDeliveryBox(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/commerce/delivery", accessToken);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            DeliveryBox value = response.Json.RootElement.GetDeliveryBox();
            return (value, response.Context);
        }
    }

    #endregion v2/commerce/delivery

    #region v2/commerce/prices

    /// <summary>Retrieves the item IDs of all items with listings on the trading post.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<int> Value, MessageContext Context)> GetItemPricesIndex(
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/commerce/prices");
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            IImmutableValueSet<int> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetInt32());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the best price for an item by its ID.</summary>
    /// <param name="itemId">The item ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(ItemPrice Value, MessageContext Context)> GetItemPriceById(
        int itemId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/commerce/prices");
        requestBuilder.Query.AddId(itemId);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ItemPrice value = response.Json.RootElement.GetItemPrice();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the best price for items by their IDs.</summary>
    /// <remarks>Limited to 200 IDs per request.</remarks>
    /// <param name="itemIds">The item IDs.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<ItemPrice> Value, MessageContext Context)> GetItemPricesByIds(
        IEnumerable<int> itemIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/commerce/prices");
        requestBuilder.Query.AddIds(itemIds);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            IImmutableValueSet<ItemPrice> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetItemPrice());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the best price for items by their IDs by chunking requests and executing them in parallel. Supports
    /// more than 200 IDs.</summary>
    /// <param name="itemIds">The item IDs.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="degreeOfParallelism">The maximum number of chunks to request in parallel.</param>
    /// <param name="chunkSize">How many IDs to request per chunk.</param>
    /// <param name="progress">A progress report provider.</param>
    /// <param name="cancellationToken">A token to cancel the request(s).</param>
    /// <returns>A task that represents the API request(s).</returns>
    public IAsyncEnumerable<(ItemPrice Value, MessageContext Context)> GetItemPricesBulk(
        IEnumerable<int> itemIds,
        MissingMemberBehavior missingMemberBehavior = default,
        int degreeOfParallelism = BulkQuery.DefaultDegreeOfParallelism,
        int chunkSize = BulkQuery.DefaultChunkSize,
        IProgress<BulkProgress>? progress = default,
        in CancellationToken cancellationToken = default
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

        async Task<IReadOnlyCollection<(ItemPrice, MessageContext)>> GetChunk(
            IEnumerable<int> chunk,
            CancellationToken cancellationToken
        )
        {
            (IImmutableValueSet<ItemPrice> values, MessageContext context) =
                await GetItemPricesByIds(chunk, missingMemberBehavior, cancellationToken)
                    .ConfigureAwait(false);
            return values.Select(value => (value, context)).ToList();
        }
    }

    /// <summary>Retrieves all item prices by chunking requests and executing them in parallel.</summary>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="degreeOfParallelism">The maximum number of chunks to request in parallel.</param>
    /// <param name="chunkSize">How many IDs to request per chunk.</param>
    /// <param name="progress">A progress report provider.</param>
    /// <param name="cancellationToken">A token to cancel the request(s).</param>
    /// <returns>A task that represents the API request(s).</returns>
    public async IAsyncEnumerable<(ItemPrice Value, MessageContext Context)> GetItemPricesBulk(
        MissingMemberBehavior missingMemberBehavior = default,
        int degreeOfParallelism = BulkQuery.DefaultDegreeOfParallelism,
        int chunkSize = BulkQuery.DefaultChunkSize,
        IProgress<BulkProgress>? progress = default,
        [EnumeratorCancellation] CancellationToken cancellationToken = default
    )
    {
        (IImmutableValueSet<int> value, _) = await GetItemPricesIndex(cancellationToken).ConfigureAwait(false);
        IAsyncEnumerable<(ItemPrice Value, MessageContext Context)> producer = GetItemPricesBulk(
            value,
            missingMemberBehavior,
            degreeOfParallelism,
            chunkSize,
            progress,
            cancellationToken
        );
        await foreach ((ItemPrice Value, MessageContext Context) itemPrice in producer.ConfigureAwait(false))
        {
            yield return itemPrice;
        }
    }

    #endregion v2/commerce/prices

    #region v2/commerce/listings

    /// <summary>Retrieves the item IDs of all items with listings on the trading post.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<int> Value, MessageContext Context)> GetOrderBooksIndex(
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/commerce/listings");
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            IImmutableValueSet<int> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetInt32());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the demand and supply of an item by its ID.</summary>
    /// <param name="itemId">The item ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(OrderBook Value, MessageContext Context)> GetOrderBookById(
        int itemId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/commerce/listings");
        requestBuilder.Query.AddId(itemId);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            OrderBook value = response.Json.RootElement.GetOrderBook();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the demand and supply of items by their IDs.</summary>
    /// <remarks>Limited to 200 IDs per request.</remarks>
    /// <param name="itemIds">The item IDs.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<OrderBook> Value, MessageContext Context)> GetOrderBooksByIds(
        IEnumerable<int> itemIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/commerce/listings");
        requestBuilder.Query.AddIds(itemIds);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            IImmutableValueSet<OrderBook> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetOrderBook());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the demand and supply of items by their IDs by chunking requests and executing them in parallel.
    /// Supports more than 200 IDs.</summary>
    /// <param name="itemIds">The item IDs.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="degreeOfParallelism">The maximum number of chunks to request in parallel.</param>
    /// <param name="chunkSize">How many IDs to request per chunk.</param>
    /// <param name="progress">A progress report provider.</param>
    /// <param name="cancellationToken">A token to cancel the request(s).</param>
    /// <returns>A task that represents the API request(s).</returns>
    public IAsyncEnumerable<(OrderBook Value, MessageContext Context)> GetOrderBooksBulk(
        IEnumerable<int> itemIds,
        MissingMemberBehavior missingMemberBehavior = default,
        int degreeOfParallelism = BulkQuery.DefaultDegreeOfParallelism,
        int chunkSize = BulkQuery.DefaultChunkSize,
        IProgress<BulkProgress>? progress = default,
        in CancellationToken cancellationToken = default
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

        async Task<IReadOnlyCollection<(OrderBook, MessageContext)>> GetChunk(
            IEnumerable<int> chunk,
            CancellationToken cancellationToken
        )
        {
            (IImmutableValueSet<OrderBook> values, MessageContext context) =
                await GetOrderBooksByIds(chunk, missingMemberBehavior, cancellationToken)
                    .ConfigureAwait(false);
            return values.Select(value => (value, context)).ToList();
        }
    }

    /// <summary>Retrieves the demand and supply of all items by chunking requests and executing them in parallel.</summary>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="degreeOfParallelism">The maximum number of chunks to request in parallel.</param>
    /// <param name="chunkSize">How many IDs to request per chunk.</param>
    /// <param name="progress">A progress report provider.</param>
    /// <param name="cancellationToken">A token to cancel the request(s).</param>
    /// <returns>A task that represents the API request(s).</returns>
    public async IAsyncEnumerable<(OrderBook Value, MessageContext Context)> GetOrderBooksBulk(
        MissingMemberBehavior missingMemberBehavior = default,
        int degreeOfParallelism = BulkQuery.DefaultDegreeOfParallelism,
        int chunkSize = BulkQuery.DefaultChunkSize,
        IProgress<BulkProgress>? progress = default,
        [EnumeratorCancellation] CancellationToken cancellationToken = default
    )
    {
        (IImmutableValueSet<int> value, _) = await GetOrderBooksIndex(cancellationToken).ConfigureAwait(false);
        IAsyncEnumerable<(OrderBook Value, MessageContext Context)> producer = GetOrderBooksBulk(
            value,
            missingMemberBehavior,
            degreeOfParallelism,
            chunkSize,
            progress,
            cancellationToken
        );
        await foreach ((OrderBook Value, MessageContext Context) orderBook in producer.ConfigureAwait(false))
        {
            yield return orderBook;
        }
    }

    #endregion v2/commerce/listings

    #region v2/commerce/exchange

    /// <summary>Retrieves the current exchange rate of gems to gold.</summary>
    /// <param name="gems">The amount of gems to exchange for gold.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(GemsToGold Value, MessageContext Context)> ExchangeGemsToGold(
        int gems,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/commerce/exchange/gems");
        requestBuilder.Query.Add("quantity", gems);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            GemsToGold value = response.Json.RootElement.GetGemsToGold();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the current exchange rate of gold to gems.</summary>
    /// <param name="gold">The amount of gold to exchange for gems.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(GoldToGems Value, MessageContext Context)> ExchangeGoldToGems(
        Coin gold,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/commerce/exchange/coins");
        requestBuilder.Query.Add("quantity", gold);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            GoldToGems value = response.Json.RootElement.GetGoldToGems();
            return (value, response.Context);
        }
    }

    #endregion v2/commerce/exchange

    #region v2/commerce/transactions

    /// <summary>Retrieves a page of current buy orders on the account. This endpoint is only accessible with a valid access
    /// token.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<Order> Value, MessageContext Context)> GetBuyOrders(
        int pageIndex,
        int? pageSize,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet(
            "v2/commerce/transactions/current/buys",
            accessToken
        );
        requestBuilder.Query.AddPage(pageIndex, pageSize);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            IImmutableValueSet<Order> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetOrder());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of current sell orders on the account. This endpoint is only accessible with a valid access
    /// token.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<Order> Value, MessageContext Context)> GetSellOrders(
        int pageIndex,
        int? pageSize,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet(
            "v2/commerce/transactions/current/sells",
            accessToken
        );
        requestBuilder.Query.AddPage(pageIndex, pageSize);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            IImmutableValueSet<Order> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetOrder());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of completed purchases on the account. This endpoint is only accessible with a valid access
    /// token.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<Transaction> Value, MessageContext Context)> GetPurchases(
        int pageIndex,
        int? pageSize,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet(
            "v2/commerce/transactions/history/buys",
            accessToken
        );
        requestBuilder.Query.AddPage(pageIndex, pageSize);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            IImmutableValueSet<Transaction> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetTransaction());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of completed sales on the account. This endpoint is only accessible with a valid access
    /// token.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<Transaction> Value, MessageContext Context)> GetSales(
        int pageIndex,
        int? pageSize,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet(
            "v2/commerce/transactions/history/sells",
            accessToken
        );
        requestBuilder.Query.AddPage(pageIndex, pageSize);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            IImmutableValueSet<Transaction> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetTransaction());
            return (value, response.Context);
        }
    }

    #endregion v2/commerce/transactions
}
