using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace GuildWars2.Emblems;

[PublicAPI]
public sealed class EmblemsQuery
{
    private readonly HttpClient http;

    public EmblemsQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/emblem/foregrounds

    public Task<Replica<HashSet<Emblem>>> GetForegroundEmblems(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ForegroundEmblemsRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<int>>> GetForegroundEmblemsIndex(
        CancellationToken cancellationToken = default
    )
    {
        var request = new ForegroundEmblemsIndexRequest();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<Emblem>> GetForegroundEmblemById(
        int foregroundEmblemId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ForegroundEmblemByIdRequest request =
            new(foregroundEmblemId) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Emblem>>> GetForegroundEmblemsByIds(
        IReadOnlyCollection<int> foregroundEmblemIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ForegroundEmblemsByIdsRequest request =
            new(foregroundEmblemIds) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Emblem>>> GetForegroundEmblemsByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ForegroundEmblemsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/emblem/backgrounds

    public Task<Replica<HashSet<Emblem>>> GetBackgroundEmblems(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        BackgroundEmblemsRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<int>>> GetBackgroundEmblemsIndex(
        CancellationToken cancellationToken = default
    )
    {
        var request = new BackgroundEmblemsIndexRequest();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<Emblem>> GetBackgroundEmblemById(
        int backgroundEmblemId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        BackgroundEmblemByIdRequest request =
            new(backgroundEmblemId) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Emblem>>> GetBackgroundEmblemsByIds(
        IReadOnlyCollection<int> backgroundEmblemIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        BackgroundEmblemsByIdsRequest request =
            new(backgroundEmblemIds) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Emblem>>> GetBackgroundEmblemsByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        BackgroundEmblemsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion
}
