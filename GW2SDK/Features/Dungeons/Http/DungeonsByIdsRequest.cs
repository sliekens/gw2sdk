using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Dungeons.Http;

internal sealed class DungeonsByIdsRequest : IHttpRequest<Replica<HashSet<Dungeon>>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/dungeons") { AcceptEncoding = "gzip" };

    public DungeonsByIdsRequest(IReadOnlyCollection<string> dungeonIds)
    {
        Check.Collection(dungeonIds);
        DungeonIds = dungeonIds;
    }

    public IReadOnlyCollection<string> DungeonIds { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<Dungeon>>> SendAsync(
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
        return new Replica<HashSet<Dungeon>>
        {
            Value = json.RootElement.GetSet(entry => entry.GetDungeon(MissingMemberBehavior)),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
