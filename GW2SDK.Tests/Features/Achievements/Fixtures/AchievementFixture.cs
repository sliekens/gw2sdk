using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Infrastructure.Achievements;
using GW2SDK.Tests.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements.Fixtures
{
    public class AchievementFixture : IAsyncLifetime
    {
        public InMemoryAchievementDb Db { get; } = new InMemoryAchievementDb();

        public async Task InitializeAsync()
        {
            var http = HttpClientFactory.CreateDefault();
            var ids = await GetAchievementIds(http);

            foreach (var subset in ids.Buffer(200))
            {
                var achievements = await GetJsonAchievementsByIds(http, subset.ToList());
                foreach (var achievement in achievements)
                {
                    Db.AddAchievement(achievement);
                }
            }
        }

        public Task DisposeAsync() => Task.CompletedTask;

        private async Task<List<int>> GetAchievementIds(HttpClient http)
        {
            using (var request = new GetAchievementIdsRequest())
            using (var response = await http.SendAsync(request))
            {
                var json = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
                return JsonConvert.DeserializeObject<List<int>>(json);
            }
        }

        private async Task<List<string>> GetJsonAchievementsByIds(HttpClient http, IReadOnlyList<int> achievementIds)
        {
            using (var request = new GetAchievementsByIdsRequest.Builder(achievementIds).GetRequest())
            using (var response = await http.SendAsync(request))
            using (var responseReader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            using (var jsonReader = new JsonTextReader(responseReader))
            {
                response.EnsureSuccessStatusCode();

                // API returns a JSON array but we want a List of JSON objects instead
                var array = await JToken.ReadFromAsync(jsonReader);
                return array.Children<JObject>().Select(achievement => achievement.ToString(Formatting.None)).ToList();
            }
        }
    }
}
