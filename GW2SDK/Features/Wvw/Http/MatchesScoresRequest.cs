﻿using GuildWars2.Http;
using GuildWars2.Json;
using GuildWars2.Wvw.Matches.Scores;

namespace GuildWars2.Wvw.Http;

internal sealed class MatchesScoresRequest : IHttpRequest<Replica<HashSet<MatchScores>>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/wvw/matches/scores")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder
        {
            { "ids", "all" },
            { "v", SchemaVersion.Recommended }
        }
    };

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<MatchScores>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(Template, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetMatchScores(MissingMemberBehavior));
        return new Replica<HashSet<MatchScores>>
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
