using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.MapChests;

[PublicAPI]
public sealed class MapChestsByIdsRequest : IHttpRequest<IReplicaSet<MapChest>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "v2/mapchests") { AcceptEncoding = "gzip" };

    public MapChestsByIdsRequest(IReadOnlyCollection<string> mapChestIds)
    {
        Check.Collection(mapChestIds, nameof(mapChestIds));
        MapChestIds = mapChestIds;
    }

    public IReadOnlyCollection<string> MapChestIds { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<MapChest>> SendAsync(
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

        return new ReplicaSet<MapChest>
        {
            Values = json.RootElement.GetSet(entry => entry.GetMapChest(MissingMemberBehavior)),
            Context = response.Headers.GetCollectionContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
