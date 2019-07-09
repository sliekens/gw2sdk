using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Accounts.Achievements.Impl;
using GW2SDK.Extensions;
using GW2SDK.Tests.TestInfrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts.Achievements.Fixtures
{
    public class AccountAchievementFixture : IAsyncLifetime
    {
        public InMemoryAccountAchievementsDb Db { get; } = new InMemoryAccountAchievementsDb();

        public async Task InitializeAsync()
        {
            var http = new Container().Resolve<IHttpClientFactory>().CreateClient("GW2SDK");
            http.UseAccessToken(ConfigurationManager.Instance.ApiKeyFull);

            // Seed InMemoryColorDb with API data for later use in integration tests
            foreach (var color in await GetAllJsonAchievements(http))
            {
                Db.AddAccountAchievement(color);
            }
        }

        public Task DisposeAsync() => Task.CompletedTask;

        private async Task<List<string>> GetAllJsonAchievements(HttpClient http)
        {
            using (var request = new GetAccountAchievementsRequest())
            using (var response = await http.SendAsync(request))
            using (var responseReader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            using (var jsonReader = new JsonTextReader(responseReader))
            {
                response.EnsureSuccessStatusCode();

                // API returns a JSON array but we want a List of JSON objects instead
                var array = await JToken.ReadFromAsync(jsonReader);
                return array.Children<JObject>().Select(obj => obj.ToString(Formatting.None)).ToList();
            }
        }
    }
}
