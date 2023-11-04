﻿using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Legends.Http;

internal sealed class LegendsByIdsRequest : IHttpRequest2<HashSet<Legend>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/legends") { AcceptEncoding = "gzip" };

    public LegendsByIdsRequest(IReadOnlyCollection<string> legendIds)
    {
        Check.Collection(legendIds);
        LegendIds = legendIds;
    }

    public IReadOnlyCollection<string> LegendIds { get; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Legend> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", LegendIds },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetLegend(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
