using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Armory;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK;

[PublicAPI]
public sealed class ArmoryQuery
{
    private readonly HttpClient http;

    public ArmoryQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region /v2/account/legendaryarmory

    public Task<IReplicaSet<BoundLegendaryItem>> GetBoundLegendaryItems(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        BoundLegendaryItemsRequest request =
            new(accessToken) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region /v2/legendaryarmory

    public Task<IReplicaSet<LegendaryItem>> GetLegendaryItems(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        LegendaryItemsRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<int>> GetLegendaryItemsIndex(
        CancellationToken cancellationToken = default
    )
    {
        LegendaryItemsIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<LegendaryItem>> GetLegendaryItemById(
        int legendaryItemId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        LegendaryItemByIdRequest request = new(legendaryItemId)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<LegendaryItem>> GetLegendaryItemsByIds(
        IReadOnlyCollection<int> legendaryItemIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        LegendaryItemsByIdsRequest request = new(legendaryItemIds)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaPage<LegendaryItem>> GetLegendaryItemsByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        LegendaryItemsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion
}
