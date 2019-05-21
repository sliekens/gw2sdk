using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Infrastructure.Accounts.Achievements;
using GW2SDK.Tests.Shared.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Accounts.Achievements
{
    public class AchievementJsonServiceTest : IClassFixture<ConfigurationFixture>
    {
        public AchievementJsonServiceTest(ConfigurationFixture configuration, ITestOutputHelper output)
        {
            _configuration = configuration;
            _output = output;
        }

        private readonly ConfigurationFixture _configuration;

        private readonly ITestOutputHelper _output;

        private AchievementJsonService CreateSut()
        {
            var http = new HttpClient
            {
                BaseAddress = _configuration.BaseAddress
            };
            http.UseAccessToken(_configuration.ApiKeyFull);
            http.UseLatestSchemaVersion();
            return new AchievementJsonService(http);
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
