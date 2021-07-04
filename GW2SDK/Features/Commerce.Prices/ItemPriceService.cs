using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;
using GW2SDK.Commerce.Prices.Http;
using GW2SDK.Http;
using GW2SDK.Json;

namespace GW2SDK.Commerce.Prices
{
    [PublicAPI]
    public sealed class ItemPriceService
    {
        private readonly HttpClient _http;

        private readonly IItemPriceReader _itemPriceReader;
        private readonly MissingMemberBehavior _missingMemberBehavior;

        public ItemPriceService(HttpClient http, IItemPriceReader itemPriceReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _itemPriceReader = itemPriceReader ?? throw new ArgumentNullException(nameof(itemPriceReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<int>> GetItemPricesIndex()
        {
            var request = new ItemPricesIndexRequest();
            return await _http.GetResourcesSet(request, json => _itemPriceReader.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<ItemPrice>> GetItemPriceById(int itemId)
        {
            var request = new ItemPriceByIdRequest(itemId);
            return await _http.GetResource(request, json => _itemPriceReader.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<ItemPrice>> GetItemPricesByIds(IReadOnlyCollection<int> itemIds)
        {
            var request = new ItemPricesByIdsRequest(itemIds);
            return await _http.GetResourcesSet(request, json => _itemPriceReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
