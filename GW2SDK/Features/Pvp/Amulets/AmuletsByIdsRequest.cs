using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Pvp.Amulets;

[PublicAPI]
public sealed class AmuletsByIdsRequest : IHttpRequest<IReplicaSet<Amulet>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "v2/pvp/amulets") { AcceptEncoding = "gzip" };

    public AmuletsByIdsRequest(IReadOnlyCollection<int> amuletIds)
    {
        Check.Collection(amuletIds, nameof(amuletIds));
        AmuletIds = amuletIds;
    }

    public IReadOnlyCollection<int> AmuletIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<Amulet>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", AmuletIds },
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

        var value = json.RootElement.GetSet(entry => entry.GetAmulet(MissingMemberBehavior));
        return new ReplicaSet<Amulet>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
