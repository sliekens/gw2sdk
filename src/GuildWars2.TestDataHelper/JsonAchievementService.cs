using System.Text.Json;

using GuildWars2.Collections;
using GuildWars2.Hero.Achievements;

namespace GuildWars2.TestDataHelper;

internal sealed class JsonAchievementService(HttpClient http)
{
    public async Task<IImmutableValueSet<string>> GetAllJsonAchievements(IProgress<BulkProgress> progress)
    {
        IImmutableValueSet<int> ids = await GetAchievementIds().ConfigureAwait(false);
        SortedDictionary<int, string> entries = [];
        await foreach ((int id, string entry) in GetJsonAchievementsByIds(ids, progress).ConfigureAwait(false))
        {
            entries[id] = entry;
        }

        return new ImmutableValueSet<string>(entries.Values);
    }

    private async Task<IImmutableValueSet<int>> GetAchievementIds()
    {
        AchievementsClient achievements = new(http);
        (IImmutableValueSet<int> ids, _) = await achievements.GetAchievementsIndex().ConfigureAwait(false);
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
            Uri resource = new("/v2/achievements", UriKind.Relative);
            BulkRequest request = new(resource) { Ids = [.. chunk] };
            JsonDocument json = await request.SendAsync(http, cancellationToken).ConfigureAwait(false);
            return [.. json.RootElement.EnumerateArray().Select(item => (item.GetProperty("id").GetInt32(), item.ToJsonLine()))];
        }
    }
}
