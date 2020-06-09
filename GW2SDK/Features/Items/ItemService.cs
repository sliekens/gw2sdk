using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Extensions;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Items.Impl;
using Newtonsoft.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    public sealed class ItemService
    {
        private readonly HttpClient _http;

        public ItemService(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IDataTransferCollection<int>> GetItemsIndex()
        {
            var request = new ItemsIndexRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<int>(context.ResultCount);
            JsonConvert.PopulateObject(json, list, Json.DefaultJsonSerializerSettings);
            return new DataTransferCollection<int>(list, context);
        }

        public async Task<Item?> GetItemById(int itemId)
        {
            var request = new ItemByIdRequest(itemId);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<Item>(json, Json.DefaultJsonSerializerSettings);
        }

        public async Task<IDataTransferCollection<Item>> GetItemsByIds(IReadOnlyCollection<int> itemIds)
        {
            if (itemIds == null)
            {
                throw new ArgumentNullException(nameof(itemIds));
            }

            if (itemIds.Count == 0)
            {
                throw new ArgumentException("Item IDs cannot be an empty collection.", nameof(itemIds));
            }

            var request = new ItemsByIdsRequest(itemIds);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Item>(context.ResultCount);
            JsonConvert.PopulateObject(json, list, Json.DefaultJsonSerializerSettings);
            return new DataTransferCollection<Item>(list, context);
        }

        public async Task<IDataTransferPage<Item>> GetItemsByPage(int pageIndex, int? pageSize = null)
        {
            var request = new ItemsByPageRequest(pageIndex, pageSize);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<Item>(pageContext.PageSize);
            JsonConvert.PopulateObject(json, list, Json.DefaultJsonSerializerSettings);
            return new DataTransferPage<Item>(list, pageContext);
        }
    }
}
