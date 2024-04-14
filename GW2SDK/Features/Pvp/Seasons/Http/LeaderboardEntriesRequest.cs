using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons.Http;

internal sealed class LeaderboardEntriesRequest(
    string seasonId,
    string boardId,
    string regionId,
    int pageIndex
) : IHttpRequest<HashSet<LeaderboardEntry>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/pvp/seasons/:id/leaderboards/:board/:region") { AcceptEncoding = "gzip" };

    public string SeasonId { get; } = seasonId;

    public string BoardId { get; } = boardId;

    public string RegionId { get; } = regionId;

    public int PageIndex { get; } = pageIndex;

    public int? PageSize { get; init; }

    
    public async Task<(HashSet<LeaderboardEntry> Value, MessageContext Context)> SendAsync(
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
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Path = Template.Path.Replace(":id", SeasonId)
                        .Replace(":board", BoardId)
                        .Replace(":region", RegionId),
                    Arguments = search
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value =
            json.RootElement.GetSet(static entry => entry.GetLeaderboardEntry());
        return (value, new MessageContext(response));
    }
}
