using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Maps;

[PublicAPI]
public sealed class ContinentsByIdsRequest : IHttpRequest<IReplicaSet<Continent>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/continents")
    {
        AcceptEncoding = "gzip"
    };

    public ContinentsByIdsRequest(IReadOnlyCollection<int> continentIds)
    {
        Check.Collection(continentIds, nameof(continentIds));
        ContinentIds = continentIds;
    }

    public IReadOnlyCollection<int> ContinentIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<Continent>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder { { "ids", ContinentIds }, { "v", SchemaVersion.Recommended } },
                    AcceptLanguage = Language?.Alpha2Code
                },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(entry => entry.GetContinent(MissingMemberBehavior));
        return new ReplicaSet<Continent>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
