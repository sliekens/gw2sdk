using GW2SDK.Features.Accounts.Achievements;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Shared;
using GW2SDK.Tests.Shared.Fixtures;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Accounts.Achievements
{
    public class AchievementTest : IClassFixture<HttpFixture>
    {
        public AchievementTest(HttpFixture http, ITestOutputHelper output)
        {
            _http = http;
            _output = output;
        }

        private readonly HttpFixture _http;

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature", "Accounts.Achievements")]
        [Trait("Category", "Integration")]
        [Trait("Importance", "Critical")]
        public void Achievement_ShouldHaveNoMissingMembers()
        {
            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var sut = new AchievementService(_http.HttpFullAccess);

            _ = sut.GetAchievements(settings);
        }
    }
}
