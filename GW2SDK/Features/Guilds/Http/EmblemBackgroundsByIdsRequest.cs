﻿using GuildWars2.Guilds.Emblems;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Http;

internal sealed class EmblemBackgroundsByIdsRequest : IHttpRequest<HashSet<EmblemBackground>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/emblem/backgrounds") { AcceptEncoding = "gzip" };

    public EmblemBackgroundsByIdsRequest(IReadOnlyCollection<int> backgroundEmblemIds)
    {
        Check.Collection(backgroundEmblemIds);
        BackgroundEmblemIds = backgroundEmblemIds;
    }

    public IReadOnlyCollection<int> BackgroundEmblemIds { get; }

    
    public async Task<(HashSet<EmblemBackground> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", BackgroundEmblemIds },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value =
            json.RootElement.GetSet(static entry => entry.GetEmblemBackground());
        return (value, new MessageContext(response));
    }
}
