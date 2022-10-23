using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Races;

[PublicAPI]
public sealed class RacesByIdsRequest : IHttpRequest<IReplicaSet<Race>>
{
    private static readonly HttpRequestMessageTemplate Template = new(HttpMethod.Get, "/v2/races")
    {
        AcceptEncoding = "gzip"
    };

    public RacesByIdsRequest(IReadOnlyCollection<RaceName> raceIds)
    {
        Check.Collection(raceIds, nameof(raceIds));
        RaceIds = raceIds;
    }

    public IReadOnlyCollection<RaceName> RaceIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<Race>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", RaceIds.Select(id => id.ToString()) },
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

        var value = json.RootElement.GetSet(entry => entry.GetRace(MissingMemberBehavior));
        return new ReplicaSet<Race>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
