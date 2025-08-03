﻿using System.Text.Json;

using GuildWars2.Hero.Equipment.Wardrobe;

namespace GuildWars2.TestDataHelper;

internal sealed class JsonSkinService(HttpClient http)
{
    public async Task<ISet<string>> GetAllJsonSkins(IProgress<BulkProgress> progress)
    {
        HashSet<int> ids = await GetSkinIds().ConfigureAwait(false);
        SortedDictionary<int, string> entries = new();
        await foreach ((int id, string entry) in GetJsonSkinsByIds(ids, progress).ConfigureAwait(false))
        {
            entries[id] = entry;
        }

        return entries.Values.ToHashSet();
    }

    private async Task<HashSet<int>> GetSkinIds()
    {
        WardrobeClient wardrobe = new(http);
        (HashSet<int> ids, _) = await wardrobe.GetSkinsIndex().ConfigureAwait(false);
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
            Uri resource = new("/v2/skins", UriKind.Relative);
            BulkRequest request = new(resource) { Ids = [.. chunk] };
            JsonDocument json = await request.SendAsync(http, cancellationToken).ConfigureAwait(false);
            return [.. json.RootElement.EnumerateArray().Select(item => (item.GetProperty("id").GetInt32(), item.ToJsonLine()))];
        }
    }
}
