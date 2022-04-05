using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.ItemStats.Http;
using GW2SDK.ItemStats.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.ItemStats
{
    [PublicAPI]
    public sealed class ItemStatService
    {
        private readonly HttpClient http;

        public ItemStatService(HttpClient http)
        {
            this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IReplicaSet<ItemStat>> GetItemStats(
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new ItemStatsRequest(language);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => ItemStatReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetItemStatsIndex(CancellationToken cancellationToken = default)
        {
            var request = new ItemStatsIndexRequest();
            return await http.GetResourcesSet(request, json => json.RootElement.GetInt32Array(), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<ItemStat>> GetItemStatById(
            int itemStatId,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new ItemStatByIdRequest(itemStatId, language);
            return await http.GetResource(request,
                    json => ItemStatReader.Read(json.RootElement, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<ItemStat>> GetItemStatsByIds(
#if NET
            IReadOnlySet<int> itemStatIds,
#else
            IReadOnlyCollection<int> itemStatIds,
#endif
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new ItemStatsByIdsRequest(itemStatIds, language);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => ItemStatReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<ItemStat>> GetItemStatsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new ItemStatsByPageRequest(pageIndex, pageSize, language);
            return await http.GetResourcesPage(request,
                    json => json.RootElement.GetArray(item => ItemStatReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>Retrieves a page, using a hyperlink obtained from a previous page result.</summary>
        /// <param name="href">One of <see cref="IPageContext.First" />, <see cref="IPageContext.Previous" />,
        /// <see cref="IPageContext.Self" />, <see cref="IPageContext.Next" /> or <see cref="IPageContext.Last" />.</param>
        /// <param name="language"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The page specified by the hyperlink.</returns>
        public async Task<IReplicaPage<ItemStat>> GetItemStatsByPage(
            HyperlinkReference href,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new PageRequest(href, language);
            return await http.GetResourcesPage(request,
                    json => json.RootElement.GetArray(item => ItemStatReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
