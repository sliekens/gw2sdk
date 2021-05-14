using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;
using GW2SDK.Http;
using GW2SDK.Items.Http;

namespace GW2SDK.Items
{
    [PublicAPI]
    public sealed class ItemService
    {
        private readonly HttpClient _http;
        private readonly IItemReader _itemReader;

        public ItemService(HttpClient http, IItemReader itemReader)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _itemReader = itemReader ?? throw new ArgumentNullException(nameof(itemReader));
        }

        public async Task<IDataTransferCollection<int>> GetItemsIndex()
        {
            var request = new ItemsIndexRequest();
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<int>(context.ResultCount);
            list.AddRange(_itemReader.Id.ReadArray(json));
            return new DataTransferCollection<int>(list, context);
        }

        public async Task<Item> GetItemById(int itemId)
        {
            var request = new ItemByIdRequest(itemId);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            return _itemReader.Read(json);
        }

        public async Task<IDataTransferCollection<Item>> GetItemsByIds(IReadOnlyCollection<int> itemIds)
        {
            if (itemIds is null)
            {
                throw new ArgumentNullException(nameof(itemIds));
            }

            if (itemIds.Count == 0)
            {
                throw new ArgumentException("Item IDs cannot be an empty collection.", nameof(itemIds));
            }

            var request = new ItemsByIdsRequest(itemIds);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Item>(context.ResultCount);
            list.AddRange(_itemReader.ReadArray(json));
            return new DataTransferCollection<Item>(list, context);
        }

        public async Task<IDataTransferPage<Item>> GetItemsByPage(int pageIndex, int? pageSize = null)
        {
            var request = new ItemsByPageRequest(pageIndex, pageSize);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<Item>(pageContext.PageSize);
            list.AddRange(_itemReader.ReadArray(json));
            return new DataTransferPage<Item>(list, pageContext);
        }
    }
}
