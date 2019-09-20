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

        public ItemPriceService([NotNull] HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IDataTransferList<int>> GetItemPricesIndex([CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetItemPricesIndexRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var listContext = response.Headers.GetListContext();
                var list = new List<int>(listContext.ResultCount);
                JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
                return new DataTransferList<int>(list, listContext);
            }
        }

        public async Task<ItemPrice> GetItemPriceById(int itemId, [CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetItemPriceByIdRequest.Builder(itemId).GetRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<ItemPrice>(json, settings ?? Json.DefaultJsonSerializerSettings);
            }
        }

        public async Task<IDataTransferList<ItemPrice>> GetItemPricesByIds([NotNull] IReadOnlyList<int> itemIds, [CanBeNull] JsonSerializerSettings settings = null)
        {
            if (itemIds == null)
            {
                throw new ArgumentNullException(nameof(itemIds));
            }

            if (itemIds.Count == 0)
            {
                throw new ArgumentException("Item IDs cannot be an empty collection.", nameof(itemIds));
            }

            using (var request = new GetItemPricesByIdsRequest.Builder(itemIds).GetRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var listContext = response.Headers.GetListContext();
                var list = new List<ItemPrice>(listContext.ResultCount);
                JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
                return new DataTransferList<ItemPrice>(list, listContext);
            }
        }
    }
}
