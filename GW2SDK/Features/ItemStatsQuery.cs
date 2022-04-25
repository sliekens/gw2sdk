using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.ItemStats;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK;

[PublicAPI]
public sealed class ItemStatsQuery
{
    private readonly HttpClient http;

    public ItemStatsQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    public Task<IReplicaSet<ItemStat>> GetItemStats(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ItemStatsRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<int>> GetItemStatsIndex(CancellationToken cancellationToken = default)
    {
        ItemStatsIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<ItemStat>> GetItemStatById(
        int itemStatId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ItemStatByIdRequest request = new(itemStatId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<ItemStat>> GetItemStatsByIds(
        IReadOnlyCollection<int> itemStatIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ItemStatsByIdsRequest request = new(itemStatIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };

        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaPage<ItemStat>> GetItemStatsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ItemStatsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }
}
