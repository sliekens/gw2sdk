using GW2SDK.Accounts.Achievements;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Tests.Features.Accounts.Achievements.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Accounts.Achievements
{
    public class AccountAchievementTest : IClassFixture<AccountAchievementFixture>
    {
        public AccountAchievementTest(AccountAchievementFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly AccountAchievementFixture _fixture;

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature",    "Accounts.Achievements")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Account_achievements_can_be_serialized_from_json()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output))
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .Build();

            AssertEx.ForEach(_fixture.Db.AccountAchievements,
                json =>
                {
                    // Next statement throws if there are missing members
                    _ = JsonConvert.DeserializeObject<AccountAchievement>(json, settings);
                });
        }
    }
}
