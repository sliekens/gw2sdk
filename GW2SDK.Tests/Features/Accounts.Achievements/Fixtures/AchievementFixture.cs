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

            var service = new AchievementJsonService(http.HttpFullAccess);

            var response = await service.GetAchievements();
            response.EnsureSuccessStatusCode();

            AchievementJsonArray = await response.Content.ReadAsStringAsync();
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}
