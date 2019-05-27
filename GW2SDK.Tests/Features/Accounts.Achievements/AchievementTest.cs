using GW2SDK.Extensions;
using GW2SDK.Features.Accounts.Achievements;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Shared;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Accounts.Achievements
{
    public class AchievementTest
    {
        public AchievementTest(ITestOutputHelper output)
        {
            _output = output;
        }

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature",    "Accounts.Achievements")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Achievement_ShouldHaveNoMissingMembers()
        {
            var http = HttpClientFactory.CreateDefault();
            http.UseAccessToken(ConfigurationManager.Instance.ApiKeyFull);

            var sut = new AchievementService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output))
                                                              .UseMissingMemberHandling(MissingMemberHandling.Error)
                                                              .Build();

            // Next statement throws if there are missing members
            _ = sut.GetAchievements(settings);
        }
    }
}
