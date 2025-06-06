﻿using GuildWars2.Hero.Achievements;

namespace GuildWars2.TestDataHelper;

public class JsonAchievementService(HttpClient http)
{
    public async Task<ISet<string>> GetAllJsonAchievements(IProgress<BulkProgress> progress)
    {
        var ids = await GetAchievementIds().ConfigureAwait(false);
        var entries = new SortedDictionary<int, string>();
        await foreach (var (id, entry) in GetJsonAchievementsByIds(ids, progress))
        {
            entries[id] = entry;
        }

        return entries.Values.ToHashSet();
    }

    private async Task<HashSet<int>> GetAchievementIds()
    {
        var achievements = new AchievementsClient(http);
        var (ids, _) = await achievements.GetAchievementsIndex();
        return ids;
    }

    public IAsyncEnumerable<(int, string)> GetJsonAchievementsByIds(
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
            var request = new BulkRequest("/v2/achievements") { Ids = chunk.ToList() };
            var json = await request.SendAsync(http, cancellationToken);
            return json.RootElement.EnumerateArray()
                .Select(item => (item.GetProperty("id").GetInt32(), item.ToJsonLine()))
                .ToList();
        }
    }
}
