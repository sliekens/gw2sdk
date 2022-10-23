using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Emblems;

[PublicAPI]
public sealed class ForegroundEmblemsByIdsRequest : IHttpRequest<IReplicaSet<Emblem>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "/v2/emblem/foregrounds") { AcceptEncoding = "gzip" };

    public ForegroundEmblemsByIdsRequest(IReadOnlyCollection<int> foregroundEmblemIds)
    {
        Check.Collection(foregroundEmblemIds, nameof(foregroundEmblemIds));
        ForegroundEmblemIds = foregroundEmblemIds;
    }

    public IReadOnlyCollection<int> ForegroundEmblemIds { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<Emblem>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", ForegroundEmblemIds },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(entry => entry.GetEmblem(MissingMemberBehavior));
        return new ReplicaSet<Emblem>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
