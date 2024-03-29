﻿using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Pve.Dungeons.Http;

internal sealed class DungeonsByIdsRequest : IHttpRequest<HashSet<Dungeon>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/dungeons") { AcceptEncoding = "gzip" };

    public DungeonsByIdsRequest(IReadOnlyCollection<string> dungeonIds)
    {
        Check.Collection(dungeonIds);
        DungeonIds = dungeonIds;
    }

    public IReadOnlyCollection<string> DungeonIds { get; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Dungeon> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", DungeonIds },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetDungeon(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
