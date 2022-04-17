using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Items.Http;
using GW2SDK.Items.Json;
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

    public async Task<IReplicaSet<int>> GetItemsIndex(CancellationToken cancellationToken = default)
    {
        ItemsIndexRequest request = new();
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
        ItemByIdRequest request = new(itemId, language);
        return await http.GetResource(request,
                json => ItemReader.Read(json.RootElement, missingMemberBehavior),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async IAsyncEnumerable<Item> GetItemsByIds(
        IReadOnlyCollection<int> itemIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        [EnumeratorCancellation] CancellationToken cancellationToken = default,
        IProgress<ICollectionContext>? progress = default
    )
    {
        var splitQuery = SplitQuery.Create<int, Item>(async (keys, ct) =>
            {
                ItemsByIdsRequest request = new(keys, language);
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
        ItemsByPageRequest request = new(pageIndex, pageSize, language);
        return await http.GetResourcesPage(request,
                json => json.RootElement.GetArray(item => ItemReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async IAsyncEnumerable<Item> GetItems(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        [EnumeratorCancellation] CancellationToken cancellationToken = default,
        IProgress<ICollectionContext>? progress = default
    )
    {
        var index = await GetItemsIndex(cancellationToken)
            .ConfigureAwait(false);
        var producer = GetItemsByIds(index.Values, language, missingMemberBehavior, cancellationToken, progress);
        await foreach (var item in producer.WithCancellation(cancellationToken)
                           .ConfigureAwait(false))
        {
            yield return item;
        }
    }
}
