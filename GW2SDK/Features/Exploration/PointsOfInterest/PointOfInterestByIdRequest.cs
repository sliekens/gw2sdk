using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GuildWars2.Exploration.PointsOfInterest;

[PublicAPI]
public sealed class PointOfInterestByIdRequest : IHttpRequest<IReplica<PointOfInterest>>
{
    private static readonly HttpRequestMessageTemplate Template = new(
        Get,
        "v2/continents/:id/floors/:floor/regions/:region/maps/:map/pois"
    ) { AcceptEncoding = "gzip" };

    public PointOfInterestByIdRequest(
        int continentId,
        int floorId,
        int regionId,
        int mapId,
        int pointOfInterestId
    )
    {
        ContinentId = continentId;
        FloorId = floorId;
        RegionId = regionId;
        MapId = mapId;
        PointOfInterestId = pointOfInterestId;
    }

    public int ContinentId { get; }

    public int FloorId { get; }

    public int RegionId { get; }

    public int MapId { get; }

    public int PointOfInterestId { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<PointOfInterest>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Path = Template.Path
                        .Replace(":id", ContinentId.ToString(CultureInfo.InvariantCulture))
                        .Replace(":floor", FloorId.ToString(CultureInfo.InvariantCulture))
                        .Replace(":region", RegionId.ToString(CultureInfo.InvariantCulture))
                        .Replace(":map", MapId.ToString(CultureInfo.InvariantCulture)),
                    Arguments = new QueryBuilder
                    {
                        { "id", PointOfInterestId },
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

        var value = json.RootElement.GetPointOfInterest(MissingMemberBehavior);
        return new Replica<PointOfInterest>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
