using System;
using System.Threading.Tasks;
using GW2SDK.Accounts.Achievements;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts.Achievements
{
    public class AccountAchievementServiceTest
    {
        private static class AccountAchievementFact
        {
            public static void Id_is_positive(AccountAchievement actual) => Assert.InRange(actual.Id, 1, int.MaxValue);
        }

        [Fact]
        [Trait("Feature",  "Accounts.Achievements")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_account_achievements()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AccountAchievementService>();

            var actual = await sut.GetAccountAchievements(ConfigurationManager.Instance.ApiKeyFull);

            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);

            Assert.All(actual.Values,
                achievement =>
                {
                    AccountAchievementFact.Id_is_positive(achievement);
                });
        }

        [Fact]
        [Trait("Feature",  "Accounts.Achievements")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_an_account_achievement_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AccountAchievementService>();

            const int achievementId = 1;

            var actual = await sut.GetAccountAchievementById(achievementId, ConfigurationManager.Instance.ApiKeyFull);

            Assert.Equal(achievementId, actual.Value.Id);
        }

        [Fact]
        [Trait("Feature",  "Accounts.Achievements")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_account_achievements_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AccountAchievementService>();

            var ids = new[] { 1, 2, 3 };

            var actual = await sut.GetAccountAchievementsByIds(ids, ConfigurationManager.Instance.ApiKeyFull);

            Assert.Collection(actual.Values, first => Assert.Equal(1, first.Id), second => Assert.Equal(2, second.Id), third => Assert.Equal(3, third.Id));
        }

        [Fact]
        [Trait("Feature",  "Accounts.Achievements")]
        [Trait("Category", "Unit")]
        public async Task Achievement_ids_cannot_be_null()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AccountAchievementService>();

            await Assert.ThrowsAsync<ArgumentNullException>("achievementIds",
                async () =>
                {
                    await sut.GetAccountAchievementsByIds(null!, ConfigurationManager.Instance.ApiKeyFull);
                });
        }

        [Fact]
        [Trait("Feature",  "Accounts.Achievements")]
        [Trait("Category", "Unit")]
        public async Task Achievement_ids_cannot_be_empty()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AccountAchievementService>();

            await Assert.ThrowsAsync<ArgumentException>("achievementIds",
                async () =>
                {
                    await sut.GetAccountAchievementsByIds(new int[0], ConfigurationManager.Instance.ApiKeyFull);
                });
        }

        [Fact]
        [Trait("Feature",  "Accounts.Achievements")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_account_achievements_by_page()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AccountAchievementService>();

            var actual = await sut.GetAccountAchievementsByPage(0, 3, ConfigurationManager.Instance.ApiKeyFull);

            Assert.Equal(3, actual.Values.Count);
            Assert.Equal(3, actual.Context.PageSize);
        }
    }
}
