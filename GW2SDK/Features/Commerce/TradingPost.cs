using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Commerce.Exchange;
using GW2SDK.Commerce.Exchange.Http;
using GW2SDK.Commerce.Listings;
using GW2SDK.Commerce.Listings.Http;
using GW2SDK.Commerce.Prices;
using GW2SDK.Commerce.Prices.Http;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Commerce
{
    [PublicAPI]
    public sealed class TradingPost
    {
        private readonly HttpClient http;

        private readonly MissingMemberBehavior missingMemberBehavior;

        private readonly ITradingPostReader tradingPostReader;

        public TradingPost(
            HttpClient http,
            ITradingPostReader tradingPostReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
            this.tradingPostReader = tradingPostReader ?? throw new ArgumentNullException(nameof(tradingPostReader));
            this.missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplica<GemsForGoldExchange>> ExchangeGemsForGold(
            int gemsCount,
            CancellationToken cancellationToken = default
        )
        {
            var request = new ExchangeGemsForGoldRequest(gemsCount);
            return await http.GetResource(request,
                    json => tradingPostReader.GemsForGold.Read(json, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<GoldForGemsExchange>> ExchangeGoldForGems(
            Coin coinsCount,
            CancellationToken cancellationToken = default
        )
        {
            var request = new ExchangeGoldForGemsRequest(coinsCount);
            return await http.GetResource(request,
                    json => tradingPostReader.GoldForGems.Read(json, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetItemPricesIndex(CancellationToken cancellationToken = default)
        {
            var request = new ItemPricesIndexRequest();
            return await http.GetResourcesSet(request,
                    json => tradingPostReader.Id.ReadArray(json, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<ItemPrice>> GetItemPriceById(
            int itemId,
            CancellationToken cancellationToken = default
        )
        {
            var request = new ItemPriceByIdRequest(itemId);
            return await http.GetResource(request,
                    json => tradingPostReader.ItemPrice.Read(json, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async IAsyncEnumerable<IReplica<ItemPrice>> GetItemPricesByIds(
#if NET
            IReadOnlySet<int> itemIds,
#else
            IReadOnlyCollection<int> itemIds,
#endif
            [EnumeratorCancellation] CancellationToken cancellationToken = default,
            IProgress<ICollectionContext>? progress = default
        )
        {
            var splitQuery = SplitQuery.Create<int, ItemPrice>(async (keys, ct) =>
                {
                    var request = new ItemPricesByIdsRequest(keys);
                    return await http.GetResourcesSet(request,
                            json => tradingPostReader.ItemPrice.ReadArray(json, missingMemberBehavior),
                            ct)
                        .ConfigureAwait(false);
                },
                progress);

            var producer = splitQuery.QueryAsync(itemIds, cancellationToken: cancellationToken);
            await foreach (var itemPrice in producer.WithCancellation(cancellationToken)
                               .ConfigureAwait(false))
            {
                yield return itemPrice;
            }
        }

        public async IAsyncEnumerable<IReplica<ItemPrice>> GetItemPrices(
            [EnumeratorCancellation] CancellationToken cancellationToken = default,
            IProgress<ICollectionContext>? progress = default
        )
        {
            var index = await GetItemPricesIndex(cancellationToken)
                .ConfigureAwait(false);
            if (!index.HasValues)
            {
                yield break;
            }

            var producer = GetItemPricesByIds(index.Values, cancellationToken, progress);
            await foreach (var itemPrice in producer.WithCancellation(cancellationToken)
                               .ConfigureAwait(false))
            {
                yield return itemPrice;
            }
        }

        public async Task<IReplicaSet<int>> GetOrderBooksIndex(CancellationToken cancellationToken = default)
        {
            var request = new OrderBooksIndexRequest();
            return await http.GetResourcesSet(request,
                    json => tradingPostReader.Id.ReadArray(json, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<OrderBook>> GetOrderBookById(
            int itemId,
            CancellationToken cancellationToken = default
        )
        {
            var request = new OrderBookByIdRequest(itemId);
            return await http.GetResource(request,
                    json => tradingPostReader.OrderBook.Read(json, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async IAsyncEnumerable<IReplica<OrderBook>> GetOrderBooksByIds(
#if NET
            IReadOnlySet<int> itemIds,
#else
            IReadOnlyCollection<int> itemIds,
#endif
            [EnumeratorCancellation] CancellationToken cancellationToken = default,
            IProgress<ICollectionContext>? progress = default
        )
        {
            var splitQuery = SplitQuery.Create<int, OrderBook>(async (keys, ct) =>
                {
                    var request = new OrderBooksByIdsRequest(keys);
                    return await http.GetResourcesSet(request,
                            json => tradingPostReader.OrderBook.ReadArray(json, missingMemberBehavior),
                            ct)
                        .ConfigureAwait(false);
                },
                progress);

            var producer = splitQuery.QueryAsync(itemIds, cancellationToken: cancellationToken);
            await foreach (var orderBook in producer.WithCancellation(cancellationToken)
                               .ConfigureAwait(false))
            {
                yield return orderBook;
            }
        }

        public async IAsyncEnumerable<IReplica<OrderBook>> GetOrderBooks(
            [EnumeratorCancellation] CancellationToken cancellationToken = default,
            IProgress<ICollectionContext>? progress = default
        )
        {
            var index = await GetOrderBooksIndex(cancellationToken)
                .ConfigureAwait(false);
            if (!index.HasValues)
            {
                yield break;
            }

            var producer = GetOrderBooksByIds(index.Values, cancellationToken, progress);
            await foreach (var orderBook in producer.WithCancellation(cancellationToken)
                               .ConfigureAwait(false))
            {
                yield return orderBook;
            }
        }
    }
}
