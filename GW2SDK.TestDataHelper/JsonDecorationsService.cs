using System.Text.Json;

using GuildWars2.Pve.Home;

namespace GuildWars2.TestDataHelper;

internal sealed class JsonDecorationsService(HttpClient http)
{
    public async Task<ISet<string>> GetAllJsonDecorations(IProgress<BulkProgress> progress)
    {
        HashSet<int> ids = await GetDecorationsIndex().ConfigureAwait(false);
        var entries = new SortedDictionary<int, string>();
        await foreach (var (id, entry) in GetJsonDecorationsByIds(ids, progress).ConfigureAwait(false))
        {
            entries[id] = entry;
        }

        return entries.Values.ToHashSet();
    }

    private async Task<HashSet<int>> GetDecorationsIndex()
    {
        var items = new HomeClient(http);
        (HashSet<int> ids, _) = await items.GetDecorationsIndex().ConfigureAwait(false);
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
            Uri resource = new Uri("/v2/home/decorations", UriKind.Relative);
            var request = new BulkRequest(resource) { Ids = [.. chunk] };
            JsonDocument json = await request.SendAsync(http, cancellationToken).ConfigureAwait(false);
            return [.. json.RootElement.EnumerateArray().Select(item => (item.GetProperty("id").GetInt32(), item.ToJsonLine()))];
        }
    }
}
