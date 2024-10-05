using GuildWars2.Pve.Home;

namespace GuildWars2.TestDataHelper;

public class JsonDecorationsService(HttpClient http)
{
    public async Task<ISet<string>> GetAllJsonDecorations(IProgress<BulkProgress> progress)
    {
        var ids = await GetDecorationsIndex().ConfigureAwait(false);
        var entries = new SortedDictionary<int, string>();
        await foreach (var (id, entry) in GetJsonDecorationsByIds(ids, progress))
        {
            entries[id] = entry;
        }

        return entries.Values.ToHashSet();
    }

    private async Task<HashSet<int>> GetDecorationsIndex()
    {
        var items = new HomeClient(http);
        var (ids, _) = await items.GetDecorationsIndex();
        return ids;
    }

    public IAsyncEnumerable<(int, string)> GetJsonDecorationsByIds(
        IEnumerable<int> ids,
        IProgress<BulkProgress>? progress = default,
        CancellationToken cancellationToken = default
    )
    {
        return BulkQuery.QueryAsync(
            ids,
            GetChunk,
            progress: progress,
            cancellationToken: cancellationToken
        );

        async Task<IReadOnlyCollection<(int, string)>> GetChunk(
            IEnumerable<int> chunk,
            CancellationToken cancellationToken
        )
        {
            var request = new BulkRequest("/v2/homestead/decorations") { Ids = chunk.ToList() };
            var json = await request.SendAsync(http, cancellationToken);
            return json.Indent(false)
                .RootElement.EnumerateArray()
                .Select(item => (item.GetProperty("id").GetInt32(), item.ToString()))
                .ToList();
        }
    }
}
