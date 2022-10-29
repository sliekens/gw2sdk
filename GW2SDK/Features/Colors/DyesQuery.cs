using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Colors;

[PublicAPI]
public sealed class DyesQuery
{
    private readonly HttpClient http;

    public DyesQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/dyes

    /// <summary>Gets the IDs of the dyes unlocked by the current account.</summary>
    [Scope(Permission.Unlocks)]
    public Task<IReplica<IReadOnlyCollection<int>>> GetUnlockedDyesIndex(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        UnlockedDyesRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/colors

    public Task<IReplicaSet<Dye>> GetColors(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ColorsRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<int>> GetColorsIndex(CancellationToken cancellationToken = default)
    {
        ColorsIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<Dye>> GetColorById(
        int colorId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ColorByIdRequest request = new(colorId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<Dye>> GetColorsByIds(
        IReadOnlyCollection<int> colorIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ColorsByIdsRequest request = new(colorIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaPage<Dye>> GetColorsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ColorsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion
}
