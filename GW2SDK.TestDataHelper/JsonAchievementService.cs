using GuildWars2.Achievements;

namespace GuildWars2.TestDataHelper;

public class JsonAchievementService
{
    private readonly HttpClient http;

    public JsonAchievementService(HttpClient http)
    {
        this.http = http;
    }

    public async Task<ISet<string>> GetAllJsonAchievements(IProgress<ResultContext> progress)
    {
        var ids = await GetAchievementIds().ConfigureAwait(false);
        var entries = new SortedDictionary<int, string>();
        await foreach (var (id, entry) in GetJsonAchievementsByIds(ids, progress))
        {
            entries[id] = entry;
        }

        return entries.Values.ToHashSet();
    }

    private async Task<HashSet<int>> GetAchievementIds() =>
        await new AchievementsIndexRequest().SendAsync(http, CancellationToken.None);

    public IAsyncEnumerable<(int, string)> GetJsonAchievementsByIds(
        IReadOnlyCollection<int> ids,
        IProgress<ResultContext>? progress = default,
        CancellationToken cancellationToken = default
    )
    {
        return BulkQuery.QueryAsync(
            ids,
            GetChunk,
            degreeOfParallelism: 100,
            progress: progress,
            cancellationToken: cancellationToken
        );

        async Task<IReadOnlyCollection<(int, string)>> GetChunk(IReadOnlyCollection<int> chunk, CancellationToken cancellationToken)
        {
            var request = new BulkRequest("/v2/achievements") { Ids = chunk };
            var json = await request.SendAsync(http, cancellationToken);
            return json.Indent(false)
                .RootElement.EnumerateArray()
                .Select(
                    item => (item.GetProperty("id").GetInt32(), item.ToString())
                )
                .ToList();
        }
    }
}
