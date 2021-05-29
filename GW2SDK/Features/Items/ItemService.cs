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

        public async Task<IDataTransferSet<int>> GetItemsIndex()
        {
            var request = new ItemsIndexRequest();
            return await _http.GetResourcesSet(request, json => _itemReader.Id.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<Item> GetItemById(int itemId)
        {
            var request = new ItemByIdRequest(itemId);
            return await _http.GetResource(request, json => _itemReader.Read(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferSet<Item>> GetItemsByIds(IReadOnlyCollection<int> itemIds)
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
            return await _http.GetResourcesSet(request, json => _itemReader.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferPage<Item>> GetItemsByPage(int pageIndex, int? pageSize = null)
        {
            var request = new ItemsByPageRequest(pageIndex, pageSize);
            return await _http.GetResourcesPage(request, json => _itemReader.ReadArray(json))
                .ConfigureAwait(false);
        }

        /// <summary>Retrieves a page, using a token obtained from a previous page result.</summary>
        /// <param name="token">One of <see cref="IPageContext.First" />, <see cref="IPageContext.Previous" />,
        /// <see cref="IPageContext.Self" />, <see cref="IPageContext.Next" /> or <see cref="IPageContext.Last" />.</param>
        /// <returns>The page specified by the token.</returns>
        public async Task<IDataTransferPage<Item>> GetItemsByPage(ContinuationToken token)
        {
            var request = new ContinuationRequest(token);
            return await _http.GetResourcesPage(request, json => _itemReader.ReadArray(json))
                .ConfigureAwait(false);
        }
    }
}
