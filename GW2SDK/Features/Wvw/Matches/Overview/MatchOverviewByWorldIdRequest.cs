﻿using GuildWars2.Http;

namespace GuildWars2.Wvw.Matches.Overview;

[PublicAPI]
public sealed class MatchOverviewByWorldIdRequest : IHttpRequest<Replica<MatchOverview>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "v2/wvw/matches/overview") { AcceptEncoding = "gzip" };

    public MatchOverviewByWorldIdRequest(int worldId)
    {
        WorldId = worldId;
    }

    public int WorldId { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<MatchOverview>> SendAsync(
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
        return new Replica<MatchOverview>
        {
            Value = json.RootElement.GetMatchOverview(MissingMemberBehavior),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
