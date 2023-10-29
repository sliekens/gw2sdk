using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.MapChests.Http;

internal sealed class MapChestsByIdsRequest : IHttpRequest<Replica<HashSet<MapChest>>>
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

    public async Task<Replica<HashSet<MapChest>>> SendAsync(
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
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        return new Replica<HashSet<MapChest>>
        {
            Value = json.RootElement.GetSet(entry => entry.GetMapChest(MissingMemberBehavior)),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
