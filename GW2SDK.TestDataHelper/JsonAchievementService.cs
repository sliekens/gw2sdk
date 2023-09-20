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
        var items = new SortedSet<string>();
        await foreach (var item in GetJsonAchievementsByIds(ids, progress))
        {
            items.Add(item);
        }

        return items;
    }

    private async Task<HashSet<int>> GetAchievementIds() =>
        await new AchievementsIndexRequest().SendAsync(http, CancellationToken.None);

    public IAsyncEnumerable<string> GetJsonAchievementsByIds(
        IReadOnlyCollection<int> ids,
        IProgress<ResultContext>? progress = default,
        CancellationToken cancellationToken = default
    )
    {
        return BulkQuery.QueryAsync(
            ids,
            GetChunk,
            degreeOfParalllelism: 100,
            progress: progress,
            cancellationToken: cancellationToken
        );

        async Task<IReadOnlyCollection<string>> GetChunk(IReadOnlyCollection<int> chunk, CancellationToken cancellationToken)
        {
            var request = new BulkRequest("/v2/achievements") { Ids = chunk };
            var json = await request.SendAsync(http, cancellationToken);
            return json.Indent(false)
                .RootElement.EnumerateArray()
                .Select(
                    item => item.ToString()
                        ?? throw new InvalidOperationException("Unexpected null in JSON array.")
                )
                .ToList();
        }
    }
}
