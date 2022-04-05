using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Items.Http;
using GW2SDK.Items.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    public sealed class ItemService
    {
        private readonly HttpClient http;

        public ItemService(HttpClient http)
        {
            this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IReplicaSet<int>> GetItemsIndex(CancellationToken cancellationToken = default)
        {
            var request = new ItemsIndexRequest();
            return await http.GetResourcesSet(request, json => json.RootElement.GetInt32Array(), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Item>> GetItemById(
            int itemId,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new ItemByIdRequest(itemId, language);
            return await http.GetResource(request,
                    json => ItemReader.Read(json.RootElement, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async IAsyncEnumerable<IReplica<Item>> GetItemsByIds(
#if NET
            IReadOnlySet<int> itemIds,
#else
            IReadOnlyCollection<int> itemIds,
#endif
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            [EnumeratorCancellation] CancellationToken cancellationToken = default,
            IProgress<ICollectionContext>? progress = default
        )
        {
            var splitQuery = SplitQuery.Create<int, Item>(async (keys, ct) =>
                {
                    var request = new ItemsByIdsRequest(keys, language);
                    return await http.GetResourcesSet(request,
                            json => json.RootElement.GetArray(item => ItemReader.Read(item, missingMemberBehavior)),
                            ct)
                        .ConfigureAwait(false);
                },
                progress);

            var producer = splitQuery.QueryAsync(itemIds, cancellationToken: cancellationToken);
            await foreach (var item in producer.WithCancellation(cancellationToken)
                               .ConfigureAwait(false))
            {
                yield return item;
            }
        }

        public async Task<IReplicaPage<Item>> GetItemsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new ItemsByPageRequest(pageIndex, pageSize, language);
            return await http.GetResourcesPage(request,
                    json => json.RootElement.GetArray(item => ItemReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>Retrieves a page, using a hyperlink obtained from a previous page result.</summary>
        /// <param name="href">One of <see cref="IPageContext.First" />, <see cref="IPageContext.Previous" />,
        /// <see cref="IPageContext.Self" />, <see cref="IPageContext.Next" /> or <see cref="IPageContext.Last" />.</param>
        /// <param name="language"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The page specified by the hyperlink.</returns>
        public async Task<IReplicaPage<Item>> GetItemsByPage(
            HyperlinkReference href,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new PageRequest(href, language);
            return await http.GetResourcesPage(request,
                    json => json.RootElement.GetArray(item => ItemReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async IAsyncEnumerable<IReplica<Item>> GetItems(
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            [EnumeratorCancellation] CancellationToken cancellationToken = default,
            IProgress<ICollectionContext>? progress = default
        )
        {
            var index = await GetItemsIndex(cancellationToken)
                .ConfigureAwait(false);
            if (!index.HasValues)
            {
                yield break;
            }

            var producer = GetItemsByIds(index.Values, language, missingMemberBehavior, cancellationToken, progress);
            await foreach (var item in producer.WithCancellation(cancellationToken)
                               .ConfigureAwait(false))
            {
                yield return item;
            }
        }
    }
}
