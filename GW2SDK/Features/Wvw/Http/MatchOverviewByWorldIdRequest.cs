using GuildWars2.Http;
using GuildWars2.Wvw.Matches.Overview;

namespace GuildWars2.Wvw.Http;

internal sealed class MatchOverviewByWorldIdRequest : IHttpRequest<MatchOverview>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/wvw/matches/overview") { AcceptEncoding = "gzip" };

    public MatchOverviewByWorldIdRequest(int worldId)
    {
        WorldId = worldId;
    }

    public int WorldId { get; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(MatchOverview Value, MessageContext Context)> SendAsync(
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
        var value = json.RootElement.GetMatchOverview(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
