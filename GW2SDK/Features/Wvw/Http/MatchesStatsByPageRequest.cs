using GuildWars2.Http;
using GuildWars2.Json;
using GuildWars2.Wvw.Matches.Stats;

namespace GuildWars2.Wvw.Http;

internal sealed class MatchesStatsByPageRequest(int pageIndex) : IHttpRequest<HashSet<MatchStats>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/wvw/matches/stats") { AcceptEncoding = "gzip" };

    public int PageIndex { get; } = pageIndex;

    public int? PageSize { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<MatchStats> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new() { { "page", PageIndex } };
        if (PageSize.HasValue)
        {
            search.Add("page_size", PageSize.Value);
        }

        search.Add("v", SchemaVersion.Recommended);
        using var response = await httpClient.SendAsync(Template with { Arguments = search }, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetMatchStats(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
