using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Achievements.Dailies;
using GW2SDK.Achievements.Dailies.Impl;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements.Dailies.Fixtures
{
    public class DailyAchievementFixture : IAsyncLifetime
    {
        public string Today { get; private set; }

        public string Tomorrow { get; private set; }

        public async Task InitializeAsync()
        {
            await using var container = new Container();
            var http = container.Resolve<IHttpClientFactory>().CreateClient("GW2SDK");

            using var today = await http.SendAsync(new DailyAchievementsRequest());
            Today = await today.Content.ReadAsStringAsync();

            using var tomorrow = await http.SendAsync(new DailyAchievementsRequest(Day.Tomorrow));
            Tomorrow = await tomorrow.Content.ReadAsStringAsync();
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
