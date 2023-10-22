﻿using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Emblems.Http;

internal sealed class ForegroundEmblemsIndexRequest : IHttpRequest<Replica<HashSet<int>>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/emblem/foregrounds")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
    };

    public async Task<Replica<HashSet<int>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        return new Replica<HashSet<int>>
        {
            Value = json.RootElement.GetSet(entry => entry.GetInt32()),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}