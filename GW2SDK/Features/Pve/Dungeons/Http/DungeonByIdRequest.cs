using GuildWars2.Http;

namespace GuildWars2.Pve.Dungeons.Http;

internal sealed class DungeonByIdRequest(string dungeonId) : IHttpRequest<Dungeon>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/dungeons") { AcceptEncoding = "gzip" };

    public string DungeonId { get; } = dungeonId;

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
