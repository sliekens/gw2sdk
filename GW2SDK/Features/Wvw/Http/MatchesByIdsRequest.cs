﻿using GuildWars2.Http;
using GuildWars2.Json;
using GuildWars2.Wvw.Matches;

namespace GuildWars2.Wvw.Http;

internal sealed class MatchesByIdsRequest : IHttpRequest<Replica<HashSet<Match>>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/wvw/matches") { AcceptEncoding = "gzip" };

    public MatchesByIdsRequest(IReadOnlyCollection<string> matchIds)
    {
        Check.Collection(matchIds);
        MatchIds = matchIds;
    }

    public IReadOnlyCollection<string> MatchIds { get; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<Match>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", MatchIds },
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
        return new Replica<HashSet<Match>>
        {
            Value = json.RootElement.GetSet(entry => entry.GetMatch(MissingMemberBehavior)),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
