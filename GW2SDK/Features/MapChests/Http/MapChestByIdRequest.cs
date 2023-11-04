using GuildWars2.Http;

namespace GuildWars2.MapChests.Http;

internal sealed class MapChestByIdRequest : IHttpRequest2<MapChest>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/mapchests") { AcceptEncoding = "gzip" };

    public MapChestByIdRequest(string mapChestId)
    {
        MapChestId = mapChestId;
    }

    public string MapChestId { get; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(MapChest Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", MapChestId },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetMapChest(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
