using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;
using GW2SDK.Commerce.Prices.Http;
using GW2SDK.Http;

namespace GW2SDK.Commerce.Prices
{
    [PublicAPI]
    public sealed class ItemPriceService
    {
        private readonly HttpClient _http;

        private readonly IItemPriceReader _itemPriceReader;

        public ItemPriceService(HttpClient http, IItemPriceReader itemPriceReader)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _itemPriceReader = itemPriceReader ?? throw new ArgumentNullException(nameof(itemPriceReader));
        }

        public async Task<IDataTransferSet<int>> GetItemPricesIndex()
        {
            var request = new ItemPricesIndexRequest();
            return await _http.GetResourcesSet(request, json => _itemPriceReader.Id.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<ItemPrice> GetItemPriceById(int itemId)
        {
            var request = new ItemPriceByIdRequest(itemId);
            return await _http.GetResource(request, json => _itemPriceReader.Read(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferSet<ItemPrice>> GetItemPricesByIds(IReadOnlyCollection<int> itemIds)
        {
            if (itemIds is null)
            {
                throw new ArgumentNullException(nameof(itemIds));
            }

            if (itemIds.Count == 0)
            {
                throw new ArgumentException("Item IDs cannot be an empty collection.", nameof(itemIds));
            }

            var request = new ItemPricesByIdsRequest(itemIds);
            return await _http.GetResourcesSet(request, json => _itemPriceReader.ReadArray(json))
                .ConfigureAwait(false);
        }
    }
}
