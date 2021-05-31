using System.Text.Json;
using GW2SDK.Accounts.Achievements;
using GW2SDK.Json;
using GW2SDK.Tests.Features.Accounts.Achievements.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts.Achievements
{
    public class AccountAchievementTest : IClassFixture<AccountAchievementFixture>
    {
        public AccountAchievementTest(AccountAchievementFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly AccountAchievementFixture _fixture;


        private static class AccountAchievementFact
        {
            public static void Id_is_positive(AccountAchievement actual) => Assert.InRange(actual.Id, 1, int.MaxValue);
        }

        [Fact]
        [Trait("Feature",    "Accounts.Achievements")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Account_achievements_can_be_created_from_json()
        {
            var sut = new AccountAchievementReader();

            AssertEx.ForEach(_fixture.AccountAchievements,
                json =>
                {
                    using var document = JsonDocument.Parse(json);

                    var actual = sut.Read(document.RootElement, MissingMemberBehavior.Error);
                    
                    AccountAchievementFact.Id_is_positive(actual);
                });
        }
    }
}
