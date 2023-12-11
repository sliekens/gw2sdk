using GuildWars2.Http;
using GuildWars2.Wvw.Matches;

namespace GuildWars2.Wvw.Http;

internal sealed class MatchByIdRequest(string matchId) : IHttpRequest<Match>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/wvw/matches") { AcceptEncoding = "gzip" };

    public string MatchId { get; } = matchId;

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Match Value, MessageContext Context)> SendAsync(
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
        var value = json.RootElement.GetMatch(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
