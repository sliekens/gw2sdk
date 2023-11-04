using GuildWars2.Http;

namespace GuildWars2.Dungeons.Http;

internal sealed class DungeonByIdRequest : IHttpRequest<Dungeon>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/dungeons") { AcceptEncoding = "gzip" };

    public DungeonByIdRequest(string dungeonId)
    {
        DungeonId = dungeonId;
    }

    public string DungeonId { get; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Dungeon Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", DungeonId },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetDungeon(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
