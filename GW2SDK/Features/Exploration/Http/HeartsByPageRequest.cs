﻿using System.Globalization;
using GuildWars2.Exploration.Hearts;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Exploration.Http;

internal sealed class HeartsByPageRequest : IHttpRequest<Replica<HashSet<Heart>>>
{
    private static readonly HttpRequestMessageTemplate Template = new(
        Get,
        "v2/continents/:id/floors/:floor/regions/:region/maps/:map/tasks"
    ) { AcceptEncoding = "gzip" };

    public HeartsByPageRequest(int continentId, int floorId, int regionId, int mapId, int pageIndex)
    {
        ContinentId = continentId;
        FloorId = floorId;
        RegionId = regionId;
        MapId = mapId;
        PageIndex = pageIndex;
    }

    public int ContinentId { get; }

    public int FloorId { get; }

    public int RegionId { get; }

    public int MapId { get; }

    public int PageIndex { get; }

    public int? PageSize { get; init; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<Heart>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new() { { "page", PageIndex } };
        if (PageSize.HasValue)
        {
            search.Add("page_size", PageSize.Value);
        }

        search.Add("v", SchemaVersion.Recommended);
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Path = Template.Path.Replace(":id", ContinentId.ToString(CultureInfo.InvariantCulture)).Replace(":floor", FloorId.ToString(CultureInfo.InvariantCulture)).Replace(":region", RegionId.ToString(CultureInfo.InvariantCulture)).Replace(":map", MapId.ToString(CultureInfo.InvariantCulture)),
                    Arguments = search,
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetHeart(MissingMemberBehavior));
        return new Replica<HashSet<Heart>>
        {
            Value = value,
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
