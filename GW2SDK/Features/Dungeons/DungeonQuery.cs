using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Dungeons.Http;
using GW2SDK.Dungeons.Models;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Dungeons;

[PublicAPI]
public sealed class DungeonQuery
{
    private readonly HttpClient http;

    public DungeonQuery(HttpClient http)
    {
        this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
    }

    public Task<IReplicaSet<string>> GetDungeonsIndex(CancellationToken cancellationToken = default)
    {
        DungeonsIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<Dungeon>> GetDungeonById(
        string dungeonId,
        Language? language = default,
        CancellationToken cancellationToken = default
    )
    {
        DungeonByIdRequest request = new(dungeonId)
        {
            Language = language,
            MissingMemberBehavior = MissingMemberBehavior.Error
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<Dungeon>> GetDungeonsByIds(
        IReadOnlyCollection<string> dungeonIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        DungeonsByIdsRequest request = new(dungeonIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaPage<Dungeon>> GetDungeonsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        DungeonsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };

        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<Dungeon>> GetDungeons(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        DungeonsRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

}
