using System.Threading.Tasks;
using GW2SDK.Infrastructure.Accounts.Achievements;
using GW2SDK.Tests.Shared.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Accounts.Achievements
{
    public class AchievementJsonServiceTest : IClassFixture<HttpFixture>
    {
        public AchievementJsonServiceTest(HttpFixture http, ITestOutputHelper output)
        {
            _http = http;
            _output = output;
        }

        private readonly HttpFixture _http;

        private readonly ITestOutputHelper _output;

        private AchievementJsonService CreateSut()
        {
            return new AchievementJsonService(_http.HttpFullAccess);
        }

        [Fact]
        public async Task GetAchievements_ShouldNotBeEmpty()
        {
            var sut = CreateSut();

            var response = await sut.GetAchievements();
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            _output.WriteLine(json);

            Assert.NotNull(response);
        }
    }
}
