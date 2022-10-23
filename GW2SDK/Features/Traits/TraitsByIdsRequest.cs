using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Traits;

[PublicAPI]
public sealed class TraitsByIdsRequest : IHttpRequest<IReplicaSet<Trait>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/traits")
    {
        AcceptEncoding = "gzip"
    };

    public TraitsByIdsRequest(IReadOnlyCollection<int> traitIds)
    {
        Check.Collection(traitIds, nameof(traitIds));
        TraitIds = traitIds;
    }

    public IReadOnlyCollection<int> TraitIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<Trait>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder { { "ids", TraitIds }, { "v", SchemaVersion.Recommended } },
                    AcceptLanguage = Language?.Alpha2Code
                },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(entry => entry.GetTrait(MissingMemberBehavior));
        return new ReplicaSet<Trait>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
