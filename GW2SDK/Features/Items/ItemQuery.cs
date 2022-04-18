using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Items.Http;
using GW2SDK.Items.Models;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Items;

[PublicAPI]
public sealed class ItemQuery
{
    private readonly HttpClient http;

    public ItemQuery(HttpClient http)
    {
        this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
    }

    public Task<IReplicaSet<int>> GetItemsIndex(CancellationToken cancellationToken = default)
    {
        var request = new ItemsIndexRequest();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<Item>> GetItemById(
        int itemId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ItemByIdRequest request = new(itemId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public IAsyncEnumerable<Item> GetItemsByIds(
        IReadOnlyCollection<int> itemIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default,
        IProgress<ICollectionContext>? progress = default
    )
    {
        var producer = SplitQuery.Create<int, Item>(
            (range, ct) =>
            {
                var request = new ItemsByIdsRequest(range)
                {
                    Language = language,
                    MissingMemberBehavior = missingMemberBehavior
                };

                return request.SendAsync(http, ct);
            },
            progress
            );
        return producer.QueryAsync(itemIds, cancellationToken: cancellationToken);
    }

    public Task<IReplicaPage<Item>> GetItemsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ItemsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };

        return request.SendAsync(http, cancellationToken);
    }

    public async IAsyncEnumerable<Item> GetItems(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        [EnumeratorCancellation] CancellationToken cancellationToken = default,
        IProgress<ICollectionContext>? progress = default
    )
    {
        var index = await GetItemsIndex(cancellationToken).ConfigureAwait(false);
        var producer = GetItemsByIds(
            index,
            language,
            missingMemberBehavior,
            cancellationToken,
            progress
            );
        await foreach (var item in producer.WithCancellation(cancellationToken)
                           .ConfigureAwait(false))
        {
            yield return item;
        }
    }
}
