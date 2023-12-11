using GuildWars2.Http;
using GuildWars2.Json;
using GuildWars2.Wvw.Matches.Scores;

namespace GuildWars2.Wvw.Http;

internal sealed class MatchesScoresByIdsRequest : IHttpRequest<HashSet<MatchScores>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/wvw/matches/scores") { AcceptEncoding = "gzip" };

    public MatchesScoresByIdsRequest(IReadOnlyCollection<string> matchIds)
    {
        Check.Collection(matchIds);
        MatchIds = matchIds;
    }

    public IReadOnlyCollection<string> MatchIds { get; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<MatchScores> Value, MessageContext Context)> SendAsync(
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
        var value = json.RootElement.GetSet(entry => entry.GetMatchScores(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
