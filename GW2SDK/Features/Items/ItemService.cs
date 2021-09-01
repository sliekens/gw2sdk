﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
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
        private readonly HttpClient http;

        private readonly IItemReader itemReader;

        private readonly MissingMemberBehavior missingMemberBehavior;

        public ItemService(
            HttpClient http,
            IItemReader itemReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
            this.itemReader = itemReader ?? throw new ArgumentNullException(nameof(itemReader));
            this.missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<int>> GetItemsIndex(CancellationToken cancellationToken = default)
        {
            var request = new ItemsIndexRequest();
            return await http.GetResourcesSet(request, json => itemReader.Id.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Item>> GetItemById(
            int itemId,
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new ItemByIdRequest(itemId, language);
            return await http.GetResource(request, json => itemReader.Read(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async IAsyncEnumerable<IReplica<Item>> GetItemsByIds(
#if NET
            IReadOnlySet<int> itemIds,
#else
            IReadOnlyCollection<int> itemIds,
#endif
            Language? language = default,
            IProgress<ICollectionContext>? progress = default,
            [EnumeratorCancellation] CancellationToken cancellationToken = default
        )
        {
            var splitQuery = SplitQuery.Create<int, Item>(async (keys, ct) =>
            {
                var request = new ItemsByIdsRequest(keys, language);
                return await http.GetResourcesSet(request, json => itemReader.ReadArray(json, missingMemberBehavior), ct)
                    .ConfigureAwait(false);
            }, progress);

            var producer = splitQuery.QueryAsync(itemIds, cancellationToken: cancellationToken);
            await foreach (var item in producer
                .WithCancellation(cancellationToken)
                .ConfigureAwait(false))
            {
                yield return item;
            }
        }

        public async Task<IReplicaPage<Item>> GetItemsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new ItemsByPageRequest(pageIndex, pageSize, language);
            return await http.GetResourcesPage(request, json => itemReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>Retrieves a page, using a hyperlink obtained from a previous page result.</summary>
        /// <param name="href">One of <see cref="IPageContext.First" />, <see cref="IPageContext.Previous" />,
        /// <see cref="IPageContext.Self" />, <see cref="IPageContext.Next" /> or <see cref="IPageContext.Last" />.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The page specified by the hyperlink.</returns>
        public async Task<IReplicaPage<Item>> GetItemsByPage(
            HyperlinkReference href,
            CancellationToken cancellationToken = default
        )
        {
            var request = new PageRequest(href);
            return await http.GetResourcesPage(request, json => itemReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async IAsyncEnumerable<IReplica<Item>> GetItems(
            Language? language = default,
            IProgress<ICollectionContext>? progress = default,
            [EnumeratorCancellation] CancellationToken cancellationToken = default
        )
        {
            var index = await GetItemsIndex(cancellationToken)
                .ConfigureAwait(false);
            if (!index.HasValues)
            {
                yield break;
            }

            var producer = GetItemsByIds(index.Values, language, progress, cancellationToken);
            await foreach (var item in producer
                .WithCancellation(cancellationToken)
                .ConfigureAwait(false))
            {
                yield return item;
            }
        }
    }
}
