using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.ItemStats.Http;
using GW2SDK.ItemStats.Json;
using GW2SDK.ItemStats.Models;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.ItemStats;

[PublicAPI]
public sealed class ItemStatQuery
{
    private readonly HttpClient http;

    public ItemStatQuery(HttpClient http)
    {
        this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
    }

    public async Task<IReplicaSet<ItemStat>> GetItemStats(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ItemStatsRequest request = new(language);
        return await http.GetResourcesSet(request,
                json => json.RootElement.GetArray(item => ItemStatReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaSet<int>> GetItemStatsIndex(CancellationToken cancellationToken = default)
    {
        ItemStatsIndexRequest request = new();
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
        ItemStatByIdRequest request = new(itemStatId, language);
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
        ItemStatsByIdsRequest request = new(itemStatIds, language);
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
        ItemStatsByPageRequest request = new(pageIndex, pageSize, language);
        return await http.GetResourcesPage(request,
                json => json.RootElement.GetArray(item => ItemStatReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }
}
