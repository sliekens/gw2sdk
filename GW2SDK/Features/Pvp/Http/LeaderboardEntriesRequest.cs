using GuildWars2.Http;
using GuildWars2.Json;
using GuildWars2.Pvp.Seasons;

namespace GuildWars2.Pvp.Http;

[PublicAPI]
public sealed class LeaderboardEntriesRequest : IHttpRequest<Replica<HashSet<LeaderboardEntry>>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/pvp/seasons/:id/leaderboards/:board/:region") { AcceptEncoding = "gzip" };

    public LeaderboardEntriesRequest(
        string seasonId,
        string boardId,
        string regionId,
        int pageIndex
    )
    {
        SeasonId = seasonId;
        BoardId = boardId;
        RegionId = regionId;
        PageIndex = pageIndex;
    }

    public string SeasonId { get; }

    public string BoardId { get; }

    public string RegionId { get; }

    public int PageIndex { get; }

    public int? PageSize { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<LeaderboardEntry>>> SendAsync(
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
        return new Replica<HashSet<LeaderboardEntry>>
        {
            Value =
                json.RootElement.GetSet(
                    entry => entry.GetLeaderboardEntry(MissingMemberBehavior)
                ),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
