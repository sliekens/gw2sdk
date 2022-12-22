using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
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
        IReadOnlyCollection<int> itemIds,
        IProgress<ResultContext>? progress = default,
        CancellationToken cancellationToken = default
    )
    {
        var producer = SplitQuery.Create<int, string>(
            async (range, ct) =>
            {
                var request = new BulkRequest("/v2/achievements") { Ids = range };
                var json = await request.SendAsync(http, ct);
                return json.Indent(false)
                    .RootElement.EnumerateArray()
                    .Select(
                        item => item.ToString()
                            ?? throw new InvalidOperationException("Unexpected null in JSON array.")
                    )
                    .ToList();
            },
            progress
        );
        return producer.QueryAsync(itemIds, cancellationToken: cancellationToken);
    }
}
