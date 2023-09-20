﻿using GuildWars2.Http;

namespace GuildWars2.Wvw.Matches.Stats;

[PublicAPI]
public sealed class MatchStatsByWorldIdRequest : IHttpRequest<Replica<MatchStats>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/wvw/matches/stats") { AcceptEncoding = "gzip" };

    public MatchStatsByWorldIdRequest(int worldId)
    {
        WorldId = worldId;
    }

    public int WorldId { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<MatchStats>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "world", WorldId },
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
        return new Replica<MatchStats>
        {
            Value = json.RootElement.GetMatchStats(MissingMemberBehavior),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
