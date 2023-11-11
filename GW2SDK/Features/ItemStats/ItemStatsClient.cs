using GuildWars2.ItemStats.Http;

namespace GuildWars2.ItemStats;

[PublicAPI]
public sealed class ItemStatsClient
{
    private readonly HttpClient httpClient;

    public ItemStatsClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetItemStatsIndex(
        CancellationToken cancellationToken = default
    )
    {
        ItemStatsIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
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
        return request.SendAsync(httpClient, cancellationToken);
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

        return request.SendAsync(httpClient, cancellationToken);
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
        return request.SendAsync(httpClient, cancellationToken);
    }
}
