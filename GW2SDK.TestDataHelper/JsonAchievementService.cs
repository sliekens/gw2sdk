using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Achievements.Http;
using GW2SDK.Http;

namespace GW2SDK.TestDataHelper;

public class JsonAchievementService
{
    private readonly HttpClient http;

    public JsonAchievementService(HttpClient http)
    {
        this.http = http;
    }

    public async Task<ISet<string>> GetAllJsonAchievements()
    {
        var ids = await GetAchievementIds()
            .ConfigureAwait(false);

        var batches = new Queue<IEnumerable<int>>(ids.Buffer(200));

        var result = new List<string>();
        var work = new List<Task<List<string>>>();

        for (var i = 0; i < 12; i++)
        {
            if (!batches.TryDequeue(out var batch))
            {
                break;
            }

            work.Add(GetJsonAchievementsByIds(batch.ToList()));
        }

        while (work.Count > 0)
        {
            var done = await Task.WhenAny(work);
            result.AddRange(done.Result);

            work.Remove(done);

            if (batches.TryDequeue(out var batch))
            {
                work.Add(GetJsonAchievementsByIds(batch.ToList()));
            }
        }

        return new SortedSet<string>(result, StringComparer.Ordinal);
    }

    private async Task<IReadOnlyCollection<int>> GetAchievementIds()
    {
        var request = new AchievementsIndexRequest();
        var response = await request.SendAsync(http, CancellationToken.None);
        return response.Values;
    }

    private async Task<List<string>> GetJsonAchievementsByIds(IReadOnlyCollection<int> achievementIds)
    {
        var request = new BulkRequest("/v2/achievements", achievementIds);
        var json = await request.SendAsync(http, CancellationToken.None)
            .ConfigureAwait(false);
        return json.Indent(false)
            .RootElement.EnumerateArray()
            .Select(item => item.ToString() ?? throw new InvalidOperationException("Unexpected null in JSON array."))
            .ToList();
    }
}
