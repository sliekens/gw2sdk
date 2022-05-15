using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Items;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK;

[PublicAPI]
public sealed class ItemsQuery
{
    private readonly HttpClient http;

    public ItemsQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
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
        IProgress<ICollectionContext>? progress = default,
        CancellationToken cancellationToken = default
    )
    {
        var producer = SplitQuery.Create<int, Item>(
            async (range, ct) =>
            {
                var request = new ItemsByIdsRequest(range)
                {
                    Language = language,
                    MissingMemberBehavior = missingMemberBehavior
                };
                var response = await request.SendAsync(http, ct).ConfigureAwait(false);
                return response.Values;
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
        IProgress<ICollectionContext>? progress = default,
        [EnumeratorCancellation] CancellationToken cancellationToken = default
    )
    {
        var index = await GetItemsIndex(cancellationToken).ConfigureAwait(false);
        var producer = GetItemsByIds(
            index,
            language,
            missingMemberBehavior,
            progress,
            cancellationToken
        );
        await foreach (var item in producer.WithCancellation(cancellationToken)
            .ConfigureAwait(false))
        {
            yield return item;
        }
    }
}
