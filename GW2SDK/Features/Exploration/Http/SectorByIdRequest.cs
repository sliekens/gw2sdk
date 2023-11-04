﻿using System.Globalization;
using GuildWars2.Exploration.Sectors;
using GuildWars2.Http;

namespace GuildWars2.Exploration.Http;

internal sealed class SectorByIdRequest : IHttpRequest2<Sector>
{
    private static readonly HttpRequestMessageTemplate Template = new(
        Get,
        "v2/continents/:id/floors/:floor/regions/:region/maps/:map/sectors"
    ) { AcceptEncoding = "gzip" };

    public SectorByIdRequest(int continentId, int floorId, int regionId, int mapId, int sectorId)
    {
        ContinentId = continentId;
        FloorId = floorId;
        RegionId = regionId;
        MapId = mapId;
        SectorId = sectorId;
    }

    public int ContinentId { get; }

    public int FloorId { get; }

    public int RegionId { get; }

    public int MapId { get; }

    public int SectorId { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Sector Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Path = Template.Path.Replace(":id", ContinentId.ToString(CultureInfo.InvariantCulture)).Replace(":floor", FloorId.ToString(CultureInfo.InvariantCulture)).Replace(":region", RegionId.ToString(CultureInfo.InvariantCulture)).Replace(":map", MapId.ToString(CultureInfo.InvariantCulture)),
                    Arguments = new QueryBuilder
                    {
                        { "id", SectorId },
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
        var value = json.RootElement.GetSector(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
