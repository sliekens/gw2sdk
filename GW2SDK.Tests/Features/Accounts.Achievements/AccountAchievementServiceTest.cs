using System;
using System.Linq;
using System.Threading.Tasks;
using GW2SDK.Features.Accounts.Achievements;
using GW2SDK.Tests.Shared;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts.Achievements
{
    public class AccountAchievementServiceTest
    {
        [Fact]
        [Trait("Feature",  "Accounts.Achievements")]
        [Trait("Category", "Integration")]
        public async Task GetAccountAchievementById_ShouldReturnAccountAchievement()
        {
            var services = new Container(ConfigurationManager.Instance.ApiKeyFull);
            var sut = services.Resolve<AccountAchievementService>();

            // Randomly chosen
            var achievementId = 1;

            var actual = await sut.GetAccountAchievementById(achievementId);

            Assert.IsType<AccountAchievement>(actual);
        }

        [Fact]
        [Trait("Feature",  "Accounts.Achievements")]
        [Trait("Category", "Integration")]
        public async Task GetAccountAchievementsByIds_ShouldReturnExpectedRange()
        {
            var services = new Container(ConfigurationManager.Instance.ApiKeyFull);
            var sut = services.Resolve<AccountAchievementService>();

            // Randomly chosen
            var ids = new[] { 1, 2, 3, 4, 5 };

            var actual = await sut.GetAccountAchievementsByIds(ids);

            Assert.Equal(ids, actual.Select(accountAchievement => accountAchievement.Id));
        }

        [Fact]
        [Trait("Feature",  "Accounts.Achievements")]
        [Trait("Category", "Unit")]
        public async Task GetAccountAchievementsByIds_WithIdsNull_ShouldThrowArgumentNullException()
        {
            var services = new Container(ConfigurationManager.Instance.ApiKeyFull);
            var sut = services.Resolve<AccountAchievementService>();

            await Assert.ThrowsAsync<ArgumentNullException>("achievementIds",
                async () =>
                {
                    await sut.GetAccountAchievementsByIds(null);
                });
        }

        [Fact]
        [Trait("Feature",  "Accounts.Achievements")]
        [Trait("Category", "Unit")]
        public async Task GetAccountAchievementsByIds_WithIdsEmpty_ShouldThrowArgumentException()
        {
            var services = new Container(ConfigurationManager.Instance.ApiKeyFull);
            var sut = services.Resolve<AccountAchievementService>();

            await Assert.ThrowsAsync<ArgumentException>("achievementIds",
                async () =>
                {
                    await sut.GetAccountAchievementsByIds(Enumerable.Empty<int>().ToList());
                });
        }

        [Fact]
        [Trait("Feature",  "Accounts.Achievements")]
        [Trait("Category", "Integration")]
        public async Task GetAccountAchievements_ShouldReturnAllAccountAchievements()
        {
            var services = new Container(ConfigurationManager.Instance.ApiKeyFull);
            var sut = services.Resolve<AccountAchievementService>();

            var actual = await sut.GetAccountAchievements();

            Assert.Equal(actual.Count, actual.ResultTotal);
            Assert.Equal(actual.Count, actual.ResultCount);
        }

        [Fact]
        [Trait("Feature",  "Accounts.Achievements")]
        [Trait("Category", "Integration")]
        public async Task GetAccountAchievementsByPage_ShouldThrowArgumentExceptionForInvalidPage()
        {
            var services = new Container(ConfigurationManager.Instance.ApiKeyFull);
            var sut = services.Resolve<AccountAchievementService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetAccountAchievementsByPage(-1, 50));
        }

        [Fact]
        [Trait("Feature",  "Accounts.Achievements")]
        [Trait("Category", "Integration")]
        public async Task GetAccountAchievementsByPage_ShouldReturnExpectedLimit()
        {
            var services = new Container(ConfigurationManager.Instance.ApiKeyFull);
            var sut = services.Resolve<AccountAchievementService>();

            var limit = 50;

            var actual = await sut.GetAccountAchievementsByPage(0, limit);

            Assert.InRange(actual.Count, 0, limit);
        }
    }
}
