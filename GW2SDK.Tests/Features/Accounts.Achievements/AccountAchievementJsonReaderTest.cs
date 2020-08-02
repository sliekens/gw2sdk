using GW2SDK.Accounts.Achievements;
using GW2SDK.Accounts.Achievements.Impl;
using GW2SDK.Tests.Features.Accounts.Achievements.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts.Achievements
{
    public class AccountAchievementJsonReaderTest : IClassFixture<AccountAchievementFixture>
    {
        public AccountAchievementJsonReaderTest(AccountAchievementFixture fixture)
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
            var sut = AccountAchievementJsonReader.Instance;
            AssertEx.ForEach(
                _fixture.Db.AccountAchievements,
                json =>
                {
                    var actual = sut.Read(json);

                    AccountAchievementFact.Id_is_positive(actual);
                }
            );
        }
    }
}
