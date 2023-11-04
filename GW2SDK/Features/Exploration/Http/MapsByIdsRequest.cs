using System.Globalization;
using GuildWars2.Exploration.Maps;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Exploration.Http;

internal sealed class MapsByIdsRequest : IHttpRequest2<HashSet<Map>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/continents/:id/floors/:floor/regions/:region/maps")
        {
            AcceptEncoding = "gzip"
        };

    public MapsByIdsRequest(
        int continentId,
        int floorId,
        int regionId,
        IReadOnlyCollection<int> mapIds
    )
    {
        Check.Collection(mapIds);
        ContinentId = continentId;
        FloorId = floorId;
        RegionId = regionId;
        MapIds = mapIds;
    }

    public int ContinentId { get; }

    public int FloorId { get; }

    public int RegionId { get; }

    public IReadOnlyCollection<int> MapIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Map> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Path = Template.Path.Replace(":id", ContinentId.ToString(CultureInfo.InvariantCulture)).Replace(":floor", FloorId.ToString(CultureInfo.InvariantCulture)).Replace(":region", RegionId.ToString(CultureInfo.InvariantCulture)),
                    Arguments = new QueryBuilder
                    {
                        { "ids", MapIds },
                        { "v", SchemaVersion.Recommended }
                    },
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetMap(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
