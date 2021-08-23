using System;
using System.Collections.Generic;
using System.Net.Http;
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

        private readonly ITradingPostReader tradingPostReader;

        private readonly MissingMemberBehavior missingMemberBehavior;

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

        public async Task<IReplica<GemsForGoldExchange>> ExchangeGemsForGold(int gemsCount)
        {
            var request = new ExchangeGemsForGoldRequest(gemsCount);
            return await http.GetResource(request, json => tradingPostReader.GemsForGold.Read(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<GoldForGemsExchange>> ExchangeGoldForGems(Coin coinsCount)
        {
            var request = new ExchangeGoldForGemsRequest(coinsCount);
            return await http.GetResource(request, json => tradingPostReader.GoldForGems.Read(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetItemPricesIndex()
        {
            var request = new ItemPricesIndexRequest();
            return await http.GetResourcesSet(request, json => tradingPostReader.Id.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<ItemPrice>> GetItemPriceById(int itemId)
        {
            var request = new ItemPriceByIdRequest(itemId);
            return await http.GetResource(request, json => tradingPostReader.ItemPrice.Read(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<ItemPrice>> GetItemPricesByIds(IReadOnlyCollection<int> itemIds)
        {
            var request = new ItemPricesByIdsRequest(itemIds);
            return await http.GetResourcesSet(request, json => tradingPostReader.ItemPrice.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetOrderBooksIndex()
        {
            var request = new OrderBooksIndexRequest();
            return await http.GetResourcesSet(request, json => tradingPostReader.Id.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<OrderBook>> GetOrderBookById(int itemId)
        {
            var request = new OrderBookByIdRequest(itemId);
            return await http.GetResource(request, json => tradingPostReader.OrderBook.Read(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<OrderBook>> GetOrderBooksByIds(IReadOnlyCollection<int> itemIds)
        {
            var request = new OrderBooksByIdsRequest(itemIds);
            return await http.GetResourcesSet(request, json => tradingPostReader.OrderBook.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
