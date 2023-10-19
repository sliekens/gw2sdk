using System.Runtime.CompilerServices;

namespace GuildWars2.Items;

[PublicAPI]
public sealed class ItemsQuery
{
    private readonly HttpClient http;

    public ItemsQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    public Task<Replica<HashSet<int>>> GetItemsIndex(CancellationToken cancellationToken = default)
    {
        var request = new ItemsIndexRequest();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<Item>> GetItemById(
        int itemId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ItemByIdRequest request = new(itemId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Item>>> GetItemsByIds(
        IReadOnlyCollection<int> itemIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ItemsByIdsRequest request = new(itemIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Item>>> GetItemsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ItemsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };

        return request.SendAsync(http, cancellationToken);
    }

    public IAsyncEnumerable<Item> GetItemsBulk(
        IReadOnlyCollection<int> itemIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        int degreeOfParalllelism = BulkQuery.DefaultDegreeOfParallelism,
        int chunkSize = BulkQuery.DefaultChunkSize,
        IProgress<ResultContext>? progress = default,
        CancellationToken cancellationToken = default
    )
    {
        return BulkQuery.QueryAsync(
            itemIds,
            GetChunk,
            degreeOfParalllelism,
            chunkSize,
            progress,
            cancellationToken
        );

        async Task<IReadOnlyCollection<Item>> GetChunk(
            IReadOnlyCollection<int> chunk,
            CancellationToken cancellationToken
        )
        {
            var result = await GetItemsByIds(
                chunk,
                language,
                missingMemberBehavior,
                cancellationToken
            );
            return result.Value;
        }
    }

    public async IAsyncEnumerable<Item> GetItemsBulk(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        int degreeOfParalllelism = BulkQuery.DefaultDegreeOfParallelism,
        int chunkSize = BulkQuery.DefaultChunkSize,
        IProgress<ResultContext>? progress = default,
        [EnumeratorCancellation] CancellationToken cancellationToken = default
    )
    {
        var index = await GetItemsIndex(cancellationToken).ConfigureAwait(false);
        var producer = GetItemsBulk(
            index.Value,
            language,
            missingMemberBehavior,
            degreeOfParalllelism,
            chunkSize,
            progress,
            cancellationToken
        );
        await foreach (var item in producer.WithCancellation(cancellationToken)
            .ConfigureAwait(false))
        {
            yield return item;
        }
    }
}
