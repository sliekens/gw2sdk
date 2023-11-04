using GuildWars2.Http;

namespace GuildWars2.MapChests.Http;

internal sealed class MapChestByIdRequest : IHttpRequest<Replica<MapChest>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/mapchests") { AcceptEncoding = "gzip" };

    public MapChestByIdRequest(string mapChestId)
    {
        MapChestId = mapChestId;
    }

    public string MapChestId { get; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<MapChest>> SendAsync(
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
        return new Replica<MapChest>
        {
            Value = value,
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
