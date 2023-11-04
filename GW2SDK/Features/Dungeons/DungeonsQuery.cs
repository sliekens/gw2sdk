﻿using GuildWars2.Dungeons.Http;

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

    public Task<(HashSet<string> Value, MessageContext Context)> GetDungeonsIndex(
        CancellationToken cancellationToken = default
    )
    {
        DungeonsIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(Dungeon Value, MessageContext Context)> GetDungeonById(
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

    public Task<(HashSet<Dungeon> Value, MessageContext Context)> GetDungeonsByIds(
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

    public Task<(HashSet<Dungeon> Value, MessageContext Context)> GetDungeonsByPage(
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

    public Task<(HashSet<Dungeon> Value, MessageContext Context)> GetDungeons(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        DungeonsRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<string> Value, MessageContext Context)> GetCompletedPaths(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        CompletedPathsRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }
}
