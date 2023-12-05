using System.Globalization;
using GuildWars2.Exploration.PointsOfInterest;
using GuildWars2.Http;

namespace GuildWars2.Exploration.Http;

internal sealed class PointOfInterestByIdRequest(
    int continentId,
    int floorId,
    int regionId,
    int mapId,
    int pointOfInterestId
) : IHttpRequest<PointOfInterest>
{
    private static readonly HttpRequestMessageTemplate Template = new(
        Get,
        "v2/continents/:id/floors/:floor/regions/:region/maps/:map/pois"
    ) { AcceptEncoding = "gzip" };

    public int ContinentId { get; } = continentId;

    public int FloorId { get; } = floorId;

    public int RegionId { get; } = regionId;

    public int MapId { get; } = mapId;

    public int PointOfInterestId { get; } = pointOfInterestId;

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(PointOfInterest Value, MessageContext Context)> SendAsync(
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
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetPointOfInterest(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
