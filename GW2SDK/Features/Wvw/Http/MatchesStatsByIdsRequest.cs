using GuildWars2.Http;
using GuildWars2.Json;
using GuildWars2.Wvw.Matches.Stats;

namespace GuildWars2.Wvw.Http;

internal sealed class MatchesStatsByIdsRequest : IHttpRequest<HashSet<MatchStats>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/wvw/matches/stats") { AcceptEncoding = "gzip" };

    public MatchesStatsByIdsRequest(IReadOnlyCollection<string> matchIds)
    {
        Check.Collection(matchIds);
        MatchIds = matchIds;
    }

    public IReadOnlyCollection<string> MatchIds { get; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<MatchStats> Value, MessageContext Context)> SendAsync(
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
        var value = json.RootElement.GetSet(entry => entry.GetMatchStats(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
