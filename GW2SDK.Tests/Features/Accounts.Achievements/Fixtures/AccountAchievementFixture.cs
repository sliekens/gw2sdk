using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Accounts.Achievements.Http;
using GW2SDK.Http;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts.Achievements.Fixtures
{
    public class AccountAchievementFixture : IAsyncLifetime
    {
        public IReadOnlyCollection<string> AccountAchievements { get; private set; }

        public async Task InitializeAsync()
        {
            await using var services = new Composer();
            var http = services.Resolve<HttpClient>();
            var json = await GetAllJsonAchievements(http, ConfigurationManager.Instance.ApiKeyFull);
            AccountAchievements = json.AsReadOnly();
        }

        public Task DisposeAsync() => Task.CompletedTask;

        private async Task<List<string>> GetAllJsonAchievements(HttpClient http, string accessToken)
        {
            var request = new AccountAchievementsRequest(accessToken);
            using var response = await http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            // API returns a JSON array but we want a List of JSON objects instead
            using var json = await response.Content.ReadAsJsonAsync();
            return json.Indent(false)
                .RootElement
                .EnumerateArray()
                .Select(item => item.ToString())
                .ToList();
        }
    }
}
