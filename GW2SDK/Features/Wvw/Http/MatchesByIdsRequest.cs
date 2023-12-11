using GuildWars2.Http;
using GuildWars2.Json;
using GuildWars2.Wvw.Matches;

namespace GuildWars2.Wvw.Http;

internal sealed class MatchesByIdsRequest : IHttpRequest<HashSet<Match>>
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

    public async Task<(HashSet<Match> Value, MessageContext Context)> SendAsync(
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
        var value = json.RootElement.GetSet(entry => entry.GetMatch(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
