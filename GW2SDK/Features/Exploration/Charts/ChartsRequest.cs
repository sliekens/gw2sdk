﻿using System.Globalization;
using GuildWars2.Http;
using GuildWars2.Json;
using static System.Net.Http.HttpMethod;

namespace GuildWars2.Exploration.Charts;

[PublicAPI]
public sealed class ChartsRequest : IHttpRequest<Replica<HashSet<Chart>>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/continents/:id/floors/:floor/regions/:region/maps")
        {
            AcceptEncoding = "gzip",
            Arguments = new QueryBuilder
            {
                { "ids", "all" },
                { "v", SchemaVersion.Recommended }
            }
        };

    public ChartsRequest(int continentId, int floorId, int regionId)
    {
        ContinentId = continentId;
        FloorId = floorId;
        RegionId = regionId;
    }

    public int ContinentId { get; }

    public int FloorId { get; }

    public int RegionId { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<Chart>>> SendAsync(
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
                        .Replace(":region", RegionId.ToString(CultureInfo.InvariantCulture)),
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        return new Replica<HashSet<Chart>>
        {
            Value = json.RootElement.GetSet(entry => entry.GetChart(MissingMemberBehavior)),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
