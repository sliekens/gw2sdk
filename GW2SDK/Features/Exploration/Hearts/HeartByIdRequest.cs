using System.Globalization;
using GuildWars2.Http;

namespace GuildWars2.Exploration.Hearts;

[PublicAPI]
public sealed class HeartByIdRequest : IHttpRequest<Replica<Heart>>
{
    private static readonly HttpRequestMessageTemplate Template = new(
        Get,
        "v2/continents/:id/floors/:floor/regions/:region/maps/:map/tasks"
    ) { AcceptEncoding = "gzip" };

    public HeartByIdRequest(int continentId, int floorId, int regionId, int mapId, int heartId)
    {
        ContinentId = continentId;
        FloorId = floorId;
        RegionId = regionId;
        MapId = mapId;
        HeartId = heartId;
    }

    public int ContinentId { get; }

    public int FloorId { get; }

    public int RegionId { get; }

    public int MapId { get; }

    public int HeartId { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<Heart>> SendAsync(
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
                        { "id", HeartId },
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
        return new Replica<Heart>
        {
            Value = json.RootElement.GetHeart(MissingMemberBehavior),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
