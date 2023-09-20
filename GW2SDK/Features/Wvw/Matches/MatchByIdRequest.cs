using GuildWars2.Http;

namespace GuildWars2.Wvw.Matches;

[PublicAPI]
public sealed class MatchByIdRequest : IHttpRequest<Replica<Match>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/wvw/matches") { AcceptEncoding = "gzip" };

    public MatchByIdRequest(string matchId)
    {
        MatchId = matchId;
    }

    public string MatchId { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<Match>> SendAsync(
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
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        return new Replica<Match>
        {
            Value = json.RootElement.GetMatch(MissingMemberBehavior),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
