using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Items.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    public sealed class ItemService
    {
        private readonly HttpClient _http;

        private readonly IItemReader _itemReader;

        private readonly MissingMemberBehavior _missingMemberBehavior;

        public ItemService(
            HttpClient http,
            IItemReader itemReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _itemReader = itemReader ?? throw new ArgumentNullException(nameof(itemReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<int>> GetItemsIndex()
        {
            var request = new ItemsIndexRequest();
            return await _http.GetResourcesSet(request, json => _itemReader.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Item>> GetItemById(int itemId, Language? language = default)
        {
            var request = new ItemByIdRequest(itemId, language);
            return await _http.GetResource(request, json => _itemReader.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Item>> GetItemsByIds(
            IReadOnlyCollection<int> itemIds,
            Language? language = default
        )
        {
            var request = new ItemsByIdsRequest(itemIds, language);
            return await _http.GetResourcesSet(request, json => _itemReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Item>> GetItemsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default
        )
        {
            var request = new ItemsByPageRequest(pageIndex, pageSize, language);
            return await _http.GetResourcesPage(request, json => _itemReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        /// <summary>Retrieves a page, using a token obtained from a previous page result.</summary>
        /// <param name="token">One of <see cref="IPageContext.First" />, <see cref="IPageContext.Previous" />,
        /// <see cref="IPageContext.Self" />, <see cref="IPageContext.Next" /> or <see cref="IPageContext.Last" />.</param>
        /// <returns>The page specified by the token.</returns>
        public async Task<IReplicaPage<Item>> GetItemsByPage(ContinuationToken token)
        {
            var request = new ContinuationRequest(token);
            return await _http.GetResourcesPage(request, json => _itemReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
