using GuildWars2.Items;

namespace GuildWars2.TestDataHelper;

public class JsonItemService(HttpClient http)
{
    public async Task<ISet<string>> GetAllJsonItems(IProgress<ResultContext> progress)
    {
        var ids = await GetItemsIndex().ConfigureAwait(false);
        var entries = new SortedDictionary<int, string>();
        await foreach (var (id, entry) in GetJsonItemsByIds(ids, progress))
        {
            entries[id] = entry;
        }

        return entries.Values.ToHashSet();
    }

    private async Task<HashSet<int>> GetItemsIndex()
    {
        var items = new ItemsClient(http);
        var (ids, _) = await items.GetItemsIndex();
        return ids;
    }

    public IAsyncEnumerable<(int, string)> GetJsonItemsByIds(
        IReadOnlyCollection<int> ids,
        IProgress<ResultContext>? progress = default,
        CancellationToken cancellationToken = default
    )
    {
        return BulkQuery.QueryAsync(
            ids,
            GetChunk,
            100,
            progress: progress,
            cancellationToken: cancellationToken
        );

        async Task<IReadOnlyCollection<(int, string)>> GetChunk(
            IReadOnlyCollection<int> chunk,
            CancellationToken cancellationToken
        )
        {
            var request = new BulkRequest("/v2/items") { Ids = chunk };
            var json = await request.SendAsync(http, cancellationToken);
            return json.Indent(false)
                .RootElement.EnumerateArray()
                .Select(item => (item.GetProperty("id").GetInt32(), item.ToString()))
                .ToList();
        }
    }
}
