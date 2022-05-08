using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Dungeons;

[PublicAPI]
public sealed class DungeonsByIdsRequest : IHttpRequest<IReplicaSet<Dungeon>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "/v2/dungeons") { AcceptEncoding = "gzip" };

    public DungeonsByIdsRequest(IReadOnlyCollection<string> dungeonIds)
    {
        Check.Collection(dungeonIds, nameof(dungeonIds));
        DungeonIds = dungeonIds;
    }

    public IReadOnlyCollection<string> DungeonIds { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<Dungeon>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with { Arguments = new QueryBuilder { { "ids", DungeonIds } } },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(
            entry => DungeonReader.GetDungeon(entry, MissingMemberBehavior)
        );
        return new ReplicaSet<Dungeon>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
