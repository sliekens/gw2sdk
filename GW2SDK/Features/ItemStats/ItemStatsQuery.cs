using GuildWars2.ItemStats.Http;

namespace GuildWars2.ItemStats;

[PublicAPI]
public sealed class ItemStatsQuery
{
    private readonly HttpClient http;

    public ItemStatsQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    public Task<(HashSet<ItemStat> Value, MessageContext Context)> GetItemStats(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ItemStatsRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetItemStatsIndex(
        CancellationToken cancellationToken = default
    )
    {
        ItemStatsIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(ItemStat Value, MessageContext Context)> GetItemStatById(
        int itemStatId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ItemStatByIdRequest request = new(itemStatId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<ItemStat> Value, MessageContext Context)> GetItemStatsByIds(
        IReadOnlyCollection<int> itemStatIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ItemStatsByIdsRequest request = new(itemStatIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };

        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<ItemStat> Value, MessageContext Context)> GetItemStatsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ItemStatsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }
}
