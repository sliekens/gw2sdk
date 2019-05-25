using System.Threading.Tasks;
using GW2SDK.Infrastructure.Accounts.Achievements;
using GW2SDK.Tests.Shared.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts.Achievements.Fixtures
{
    public class AchievementFixture : IAsyncLifetime
    {
        public string AchievementJsonArray { get; set; }

        public async Task InitializeAsync()
        {
            var http = new HttpFixture();

            using (var request = new GetAchievementsRequest())
            using (var response = await http.HttpFullAccess.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                AchievementJsonArray = await response.Content.ReadAsStringAsync();
            }
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
