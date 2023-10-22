﻿using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Http;

internal sealed class LeaderboardsRequest : IHttpRequest<Replica<HashSet<string>>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/pvp/seasons/:id/leaderboards") { AcceptEncoding = "gzip" };

    public LeaderboardsRequest(string seasonId)
    {
        SeasonId = seasonId;
    }

    public string SeasonId { get; }

    public async Task<Replica<HashSet<string>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Path = Template.Path.Replace(":id", SeasonId),
                    Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        return new Replica<HashSet<string>>
        {
            Value = json.RootElement.GetSet(entry => entry.GetStringRequired()),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}