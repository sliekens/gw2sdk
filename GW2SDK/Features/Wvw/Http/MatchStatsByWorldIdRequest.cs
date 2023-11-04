using GuildWars2.Http;
using GuildWars2.Wvw.Matches.Stats;

namespace GuildWars2.Wvw.Http;

internal sealed class MatchStatsByWorldIdRequest : IHttpRequest<MatchStats>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/wvw/matches/stats") { AcceptEncoding = "gzip" };

    public MatchStatsByWorldIdRequest(int worldId)
    {
        WorldId = worldId;
    }

    public int WorldId { get; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(MatchStats Value, MessageContext Context)> SendAsync(
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
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetMatchStats(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
