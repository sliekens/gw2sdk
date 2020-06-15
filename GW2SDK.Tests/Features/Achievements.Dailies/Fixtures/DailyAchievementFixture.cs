using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Achievements.Dailies.Impl;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements.Dailies.Fixtures
{
    public class DailyAchievementFixture : IAsyncLifetime
    {
        public string DailyAchievements { get; set; }

        public async Task InitializeAsync()
        {
            await using var container = new Container();
            var http = container.Resolve<IHttpClientFactory>().CreateClient("GW2SDK");
            var request = new DailyAchievementsRequest();
            using var response = await http.SendAsync(request);
            DailyAchievements = await response.Content.ReadAsStringAsync();
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
