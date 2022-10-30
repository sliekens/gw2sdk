using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Exploration.Floors;

[PublicAPI]
public sealed class FloorsByIdsRequest : IHttpRequest<IReplicaSet<Floor>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/continents/:id/floors") { AcceptEncoding = "gzip" };

    public FloorsByIdsRequest(int continentId, IReadOnlyCollection<int> floorIds)
    {
        Check.Collection(floorIds, nameof(floorIds));
        ContinentId = continentId;
        FloorIds = floorIds;
    }

    public int ContinentId { get; }

    public IReadOnlyCollection<int> FloorIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<Floor>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Path = Template.Path.Replace(
                        ":id",
                        ContinentId.ToString(CultureInfo.InvariantCulture)
                    ),
                    Arguments = new QueryBuilder
                    {
                        { "ids", FloorIds },
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

        var value = json.RootElement.GetSet(entry => entry.GetFloor(MissingMemberBehavior));
        return new ReplicaSet<Floor>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
