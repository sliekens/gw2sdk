using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches.Scores;

[PublicAPI]
public sealed class MatchesScoresByIdsRequest : IHttpRequest<Replica<HashSet<MatchScores>>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/wvw/matches/scores") { AcceptEncoding = "gzip" };

    public MatchesScoresByIdsRequest(IReadOnlyCollection<string> matchIds)
    {
        Check.Collection(matchIds);
        MatchIds = matchIds;
    }

    public IReadOnlyCollection<string> MatchIds { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<MatchScores>>> SendAsync(
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
        return new Replica<HashSet<MatchScores>>
        {
            Value =
                json.RootElement.GetSet(entry => entry.GetMatchScores(MissingMemberBehavior)),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
