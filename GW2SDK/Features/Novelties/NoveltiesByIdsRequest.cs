using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Novelties;

[PublicAPI]
public sealed class NoveltiesByIdsRequest : IHttpRequest<IReplicaSet<Novelty>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "v2/novelties") { AcceptEncoding = "gzip" };

    public NoveltiesByIdsRequest(IReadOnlyCollection<int> noveltyIds)
    {
        Check.Collection(noveltyIds, nameof(noveltyIds));
        NoveltyIds = noveltyIds;
    }

    public IReadOnlyCollection<int> NoveltyIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<Novelty>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", NoveltyIds },
                        { "v", SchemaVersion.Recommended }
                    },
                    AcceptLanguage = Language?.Alpha2Code
                },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(entry => entry.GetNovelty(MissingMemberBehavior));
        return new ReplicaSet<Novelty>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
