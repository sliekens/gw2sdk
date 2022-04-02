using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Commerce.Exchange;
using GW2SDK.Commerce.Exchange.Http;
using GW2SDK.Commerce.Exchange.Json;
using GW2SDK.Commerce.Listings;
using GW2SDK.Commerce.Listings.Http;
using GW2SDK.Commerce.Listings.Json;
using GW2SDK.Commerce.Prices;
using GW2SDK.Commerce.Prices.Http;
using GW2SDK.Commerce.Prices.Json;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Commerce
{
    [PublicAPI]
    public sealed class TradingPost
    {
        private readonly HttpClient http;

        public TradingPost(HttpClient http)
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
        }

        #region Spot prices

        public async Task<IReplicaSet<int>> GetItemPricesIndex(CancellationToken cancellationToken = default)
        {
            var request = new ItemPricesIndexRequest();
            return await http.GetResourcesSet(request, json => json.RootElement.GetInt32Array(), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<ItemPrice>> GetItemPriceById(
            int itemId,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new ItemPriceByIdRequest(itemId);
            return await http.GetResource(request,
                    json => ItemPriceReader.Read(json.RootElement, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async IAsyncEnumerable<IReplica<ItemPrice>> GetItemPricesByIds(
#if NET
            IReadOnlySet<int> itemIds,
#else
            IReadOnlyCollection<int> itemIds,
#endif
            MissingMemberBehavior missingMemberBehavior = default,
            [EnumeratorCancellation] CancellationToken cancellationToken = default,
            IProgress<ICollectionContext>? progress = default
        )
        {
            var splitQuery = SplitQuery.Create<int, ItemPrice>(async (keys, ct) =>
                {
                    var request = new ItemPricesByIdsRequest(keys);
                    return await http.GetResourcesSet(request,
                            json =>
                                json.RootElement.GetArray(item => ItemPriceReader.Read(item, missingMemberBehavior)),
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
            MissingMemberBehavior missingMemberBehavior = default,
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

            var producer = GetItemPricesByIds(index.Values, missingMemberBehavior, cancellationToken, progress);
            await foreach (var itemPrice in producer.WithCancellation(cancellationToken)
                               .ConfigureAwait(false))
            {
                yield return itemPrice;
            }
        }

        #endregion

        #region Order books

        public async Task<IReplicaSet<int>> GetOrderBooksIndex(CancellationToken cancellationToken = default)
        {
            var request = new OrderBooksIndexRequest();
            return await http.GetResourcesSet(request, json => json.RootElement.GetInt32Array(), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<OrderBook>> GetOrderBookById(
            int itemId,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new OrderBookByIdRequest(itemId);
            return await http.GetResource(request,
                    json => OrderBookReader.Read(json.RootElement, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async IAsyncEnumerable<IReplica<OrderBook>> GetOrderBooksByIds(
#if NET
            IReadOnlySet<int> itemIds,
#else
            IReadOnlyCollection<int> itemIds,
#endif
            MissingMemberBehavior missingMemberBehavior = default,
            [EnumeratorCancellation] CancellationToken cancellationToken = default,
            IProgress<ICollectionContext>? progress = default
        )
        {
            var splitQuery = SplitQuery.Create<int, OrderBook>(async (keys, ct) =>
                {
                    var request = new OrderBooksByIdsRequest(keys);
                    return await http.GetResourcesSet(request,
                            json =>
                                json.RootElement.GetArray(item => OrderBookReader.Read(item, missingMemberBehavior)),
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
            MissingMemberBehavior missingMemberBehavior = default,
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

            var producer = GetOrderBooksByIds(index.Values, missingMemberBehavior, cancellationToken, progress);
            await foreach (var orderBook in producer.WithCancellation(cancellationToken)
                               .ConfigureAwait(false))
            {
                yield return orderBook;
            }
        }

        #endregion

        #region Currency Exchange

        public async Task<IReplica<GemsForGoldExchange>> ExchangeGemsForGold(
            int gemsCount,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new ExchangeGemsForGoldRequest(gemsCount);
            return await http.GetResource(request,
                    json => GemsForGoldReader.Read(json.RootElement, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<GoldForGemsExchange>> ExchangeGoldForGems(
            Coin coinsCount,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new ExchangeGoldForGemsRequest(coinsCount);
            return await http.GetResource(request,
                    json => GoldForGemsReader.Read(json.RootElement, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        #endregion
    }
}
