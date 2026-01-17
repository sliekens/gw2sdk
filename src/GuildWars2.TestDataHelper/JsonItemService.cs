using System.Text.Json;

using GuildWars2.Collections;
using GuildWars2.Items;

namespace GuildWars2.TestDataHelper;

internal sealed class JsonItemService(HttpClient http)
{
    public async Task<IImmutableValueSet<string>> GetAllJsonItems(IProgress<BulkProgress> progress)
    {
        IImmutableValueSet<int> ids = await GetItemsIndex().ConfigureAwait(false);
        SortedDictionary<int, string> entries = [];
        await foreach ((int id, string entry) in GetJsonItemsByIds(ids, progress).ConfigureAwait(false))
        {
            entries[id] = entry;
        }

        return new ImmutableValueSet<string>(entries.Values);
    }

    private async Task<IImmutableValueSet<int>> GetItemsIndex()
    {
        ItemsClient items = new(http);
        (IImmutableValueSet<int> ids, _) = await items.GetItemsIndex().ConfigureAwait(false);
        return ids;
    }

    public IAsyncEnumerable<(int, string)> GetJsonItemsByIds(
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
            Uri resource = new("/v2/items", UriKind.Relative);
            BulkRequest request = new(resource) { Ids = [.. chunk] };
            JsonDocument json = await request.SendAsync(http, cancellationToken).ConfigureAwait(false);
            return [.. json.RootElement.EnumerateArray().Select(item => (item.GetProperty("id").GetInt32(), item.ToJsonLine()))];
        }
    }
}
