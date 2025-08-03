using GuildWars2.Hero.Equipment.Wardrobe;

namespace GuildWars2.TestDataHelper;

internal sealed class JsonSkinService(HttpClient http)
{
    public async Task<ISet<string>> GetAllJsonSkins(IProgress<BulkProgress> progress)
    {
        var ids = await GetSkinIds().ConfigureAwait(false);
        var entries = new SortedDictionary<int, string>();
        await foreach (var (id, entry) in GetJsonSkinsByIds(ids, progress).ConfigureAwait(false))
        {
            entries[id] = entry;
        }

        return entries.Values.ToHashSet();
    }

    private async Task<HashSet<int>> GetSkinIds()
    {
        var wardrobe = new WardrobeClient(http);
        var (ids, _) = await wardrobe.GetSkinsIndex().ConfigureAwait(false);
        return ids;
    }

    public IAsyncEnumerable<(int, string)> GetJsonSkinsByIds(
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
            Uri resource = new Uri("/v2/skins", UriKind.Relative);
            var request = new BulkRequest(resource) { Ids = [.. chunk] };
            var json = await request.SendAsync(http, cancellationToken).ConfigureAwait(false);
            return [.. json.RootElement.EnumerateArray().Select(item => (item.GetProperty("id").GetInt32(), item.ToJsonLine()))];
        }
    }
}
