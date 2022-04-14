using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using GW2SDK.Annotations;
using GW2SDK.Colors.Http;
using GW2SDK.Colors.Json;
using GW2SDK.Colors.Models;
using GW2SDK.Crafting.Http;
using GW2SDK.Http;
using GW2SDK.Json;

using JetBrains.Annotations;

namespace GW2SDK.Colors;

[PublicAPI]
public sealed class DyeQuery
{
    private readonly HttpClient http;

    public DyeQuery(HttpClient http)
    {
        this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
    }

    #region /v2/colors

    public async Task<IReplicaSet<Dye>> GetColors(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ColorsRequest request = new(language);
        return await http.GetResourcesSet(request,
                json => json.RootElement.GetArray(item => DyeReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaSet<int>> GetColorsIndex(CancellationToken cancellationToken = default)
    {
        ColorsIndexRequest request = new();
        return await http.GetResourcesSet(request, json => json.RootElement.GetInt32Array(), cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplica<Dye>> GetColorById(
        int colorId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ColorByIdRequest request = new(colorId, language);
        return await http.GetResource(request,
                json => DyeReader.Read(json.RootElement, missingMemberBehavior),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaSet<Dye>> GetColorsByIds(
#if NET
        IReadOnlySet<int> colorIds,
#else
        IReadOnlyCollection<int> colorIds,
#endif
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ColorsByIdsRequest request = new(colorIds, language);
        return await http.GetResourcesSet(request,
                json => json.RootElement.GetArray(item => DyeReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaPage<Dye>> GetColorsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ColorsByPageRequest request = new(pageIndex, pageSize, language);
        return await http.GetResourcesPage(request,
                json => json.RootElement.GetArray(item => DyeReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }

    #endregion

    #region /v2/account/dyes


    /// <summary>Gets the IDs of the dyes unlocked by the current account.</summary>
    [Scope(Permission.Unlocks)]
#if NET
    public async Task<IReplica<IReadOnlySet<int>>> GetUnlockedDyes(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
#else
    public async Task<IReplica<IReadOnlyCollection<int>>> GetUnlockedDyes(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
#endif
    {
        UnlockedDyesRequest request = new(accessToken);
        return await http.GetResourcesSetSimple(request, json => json.RootElement.GetInt32Array(), cancellationToken)
            .ConfigureAwait(false);
    }

    #endregion
}