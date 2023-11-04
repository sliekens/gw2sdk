using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.MapChests.Http;

internal sealed class MapChestsByIdsRequest : IHttpRequest<HashSet<MapChest>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/mapchests") { AcceptEncoding = "gzip" };

    public MapChestsByIdsRequest(IReadOnlyCollection<string> mapChestIds)
    {
        Check.Collection(mapChestIds);
        MapChestIds = mapChestIds;
    }

    public IReadOnlyCollection<string> MapChestIds { get; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<MapChest> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", MapChestIds },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetMapChest(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
