﻿using GuildWars2.Exploration.Maps;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Exploration.Http;

internal sealed class MapSummariesByIdsRequest : IHttpRequest<HashSet<MapSummary>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/maps")
    {
        AcceptEncoding = "gzip"
    };

    public MapSummariesByIdsRequest(IReadOnlyCollection<int> mapIds)
    {
        Check.Collection(mapIds);
        MapIds = mapIds;
    }

    public IReadOnlyCollection<int> MapIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<MapSummary> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
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
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetMapSummary(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
