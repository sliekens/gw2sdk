using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.ItemStats.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.ItemStats
{
    [PublicAPI]
    public sealed class ItemStatService
    {
        private readonly HttpClient _http;

        private readonly IItemStatReader _itemStatReader;

        private readonly MissingMemberBehavior _missingMemberBehavior;

        public ItemStatService(
            HttpClient http,
            IItemStatReader itemStatReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _itemStatReader = itemStatReader ?? throw new ArgumentNullException(nameof(itemStatReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<ItemStat>> GetItemStats(Language? language = default)
        {
            var request = new ItemStatsRequest(language);
            return await _http.GetResourcesSet(request, json => _itemStatReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetItemStatsIndex()
        {
            var request = new ItemStatsIndexRequest();
            return await _http
                .GetResourcesSet(request, json => _itemStatReader.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<ItemStat>> GetItemStatById(int itemStatId, Language? language = default)
        {
            var request = new ItemStatByIdRequest(itemStatId, language);
            return await _http.GetResource(request, json => _itemStatReader.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<ItemStat>> GetItemStatsByIds(
            IReadOnlyCollection<int> itemStatIds,
            Language? language = default
        )
        {
            var request = new ItemStatsByIdsRequest(itemStatIds, language);
            return await _http.GetResourcesSet(request, json => _itemStatReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<ItemStat>> GetItemStatsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default
        )
        {
            var request = new ItemStatsByPageRequest(pageIndex, pageSize, language);
            return await _http
                .GetResourcesPage(request, json => _itemStatReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        /// <summary>Retrieves a page, using a token obtained from a previous page result.</summary>
        /// <param name="token">One of <see cref="IPageContext.First" />, <see cref="IPageContext.Previous" />,
        /// <see cref="IPageContext.Self" />, <see cref="IPageContext.Next" /> or <see cref="IPageContext.Last" />.</param>
        /// <returns>The page specified by the token.</returns>
        public async Task<IReplicaPage<ItemStat>> GetItemStatsByPage(ContinuationToken token)
        {
            var request = new ContinuationRequest(token);
            return await _http
                .GetResourcesPage(request, json => _itemStatReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
