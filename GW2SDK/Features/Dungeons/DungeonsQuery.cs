using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Dungeons;

[PublicAPI]
public sealed class DungeonsQuery
{
    private readonly HttpClient http;

    public DungeonsQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    public Task<IReplicaSet<string>> GetDungeonsIndex(CancellationToken cancellationToken = default)
    {
        DungeonsIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<Dungeon>> GetDungeonById(
        string dungeonId,
        CancellationToken cancellationToken = default
    )
    {
        DungeonByIdRequest request = new(dungeonId)
        {
            MissingMemberBehavior = MissingMemberBehavior.Error
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<Dungeon>> GetDungeonsByIds(
        IReadOnlyCollection<string> dungeonIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        DungeonsByIdsRequest request = new(dungeonIds)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaPage<Dungeon>> GetDungeonsByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        DungeonsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            MissingMemberBehavior = missingMemberBehavior
        };

        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<Dungeon>> GetDungeons(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        DungeonsRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    [Scope(Permission.Progression)]
    public Task<IReplica<IReadOnlyCollection<string>>> GetCompletedPaths(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        CompletedPathsRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }
}
