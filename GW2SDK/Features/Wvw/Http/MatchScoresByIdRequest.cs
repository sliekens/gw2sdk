using GuildWars2.Http;
using GuildWars2.Wvw.Matches.Scores;

namespace GuildWars2.Wvw.Http;

internal sealed class MatchScoresByIdRequest : IHttpRequest<MatchScores>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/wvw/matches/scores") { AcceptEncoding = "gzip" };

    public MatchScoresByIdRequest(string matchId)
    {
        MatchId = matchId;
    }

    public string MatchId { get; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(MatchScores Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", MatchId },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetMatchScores(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
