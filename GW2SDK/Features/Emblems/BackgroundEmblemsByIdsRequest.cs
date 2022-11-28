using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Emblems;

[PublicAPI]
public sealed class BackgroundEmblemsByIdsRequest : IHttpRequest<IReplicaSet<Emblem>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "v2/emblem/backgrounds") { AcceptEncoding = "gzip" };

    public BackgroundEmblemsByIdsRequest(IReadOnlyCollection<int> backgroundEmblemIds)
    {
        Check.Collection(backgroundEmblemIds, nameof(backgroundEmblemIds));
        BackgroundEmblemIds = backgroundEmblemIds;
    }

    public IReadOnlyCollection<int> BackgroundEmblemIds { get; }

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
                        { "ids", BackgroundEmblemIds },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(entry => EmblemJson.GetEmblem(entry, MissingMemberBehavior));
        return new ReplicaSet<Emblem>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
