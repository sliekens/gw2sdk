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

        public async Task<IDataTransferCollection<int>> GetItemPricesIndex()
        {
            var request = new ItemPricesIndexRequest();
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<int>(context.ResultCount);
            list.AddRange(_itemPriceReader.Id.ReadArray(json));
            return new DataTransferCollection<int>(list, context);
        }

        public async Task<ItemPrice> GetItemPriceById(int itemId)
        {
            var request = new ItemPriceByIdRequest(itemId);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            return _itemPriceReader.Read(json);
        }

        public async Task<IDataTransferCollection<ItemPrice>> GetItemPricesByIds(IReadOnlyCollection<int> itemIds)
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
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<ItemPrice>(context.ResultCount);
            list.AddRange(_itemPriceReader.ReadArray(json));
            return new DataTransferCollection<ItemPrice>(list, context);
        }
    }
}
