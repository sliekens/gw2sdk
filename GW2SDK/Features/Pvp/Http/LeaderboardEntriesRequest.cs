using GuildWars2.Http;
using GuildWars2.Json;
using GuildWars2.Pvp.Seasons;

namespace GuildWars2.Pvp.Http;

internal sealed class LeaderboardEntriesRequest : IHttpRequest<HashSet<LeaderboardEntry>>
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

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

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
                    Path = Template.Path.Replace(":id", SeasonId).Replace(":board", BoardId).Replace(":region", RegionId),
                    Arguments = search
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetLeaderboardEntry(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
