using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Commerce.Prices.Impl;
using GW2SDK.Extensions;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonConverters;
using Newtonsoft.Json;

namespace GW2SDK.Commerce.Prices
{
    [PublicAPI]
    public sealed class ItemPriceService
    {
        private readonly HttpClient _http;

        public ItemPriceService(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IDataTransferCollection<int>> GetItemPricesIndex()
        {
            var request = new ItemPricesIndexRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<int>(context.ResultCount);
            JsonConvert.PopulateObject(json, list, Json.DefaultJsonSerializerSettings);
            return new DataTransferCollection<int>(list, context);
        }

        public async Task<ItemPrice?> GetItemPriceById(int itemId)
        {
            var request = new ItemPriceByIdRequest(itemId);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ItemPrice>(json, Json.DefaultJsonSerializerSettings);
        }

        public async Task<IDataTransferCollection<ItemPrice>> GetItemPricesByIds(IReadOnlyCollection<int> itemIds)
        {
            if (itemIds == null)
            {
                throw new ArgumentNullException(nameof(itemIds));
            }

            if (itemIds.Count == 0)
            {
                throw new ArgumentException("Item IDs cannot be an empty collection.", nameof(itemIds));
            }

            var request = new ItemPricesByIdsRequest(itemIds);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<ItemPrice>(context.ResultCount);
            JsonConvert.PopulateObject(json, list, Json.DefaultJsonSerializerSettings);
            return new DataTransferCollection<ItemPrice>(list, context);
        }
    }
}
