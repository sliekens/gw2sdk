using System;
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
        public async Task GetAccountAchievements_ShouldReturnAllAccountAchievements()
        {
            var services = new Container(ConfigurationManager.Instance.ApiKeyFull);
            var sut = services.Resolve<AccountAchievementService>();

            var actual = await sut.GetAccountAchievements();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Accounts.Achievements")]
        [Trait("Category", "Integration")]
        public async Task GetAccountAchievementById_ShouldReturnThatAccountAchievement()
        {
            var services = new Container(ConfigurationManager.Instance.ApiKeyFull);
            var sut = services.Resolve<AccountAchievementService>();

            const int achievementId = 1;

            var actual = await sut.GetAccountAchievementById(achievementId);

            Assert.Equal(achievementId, actual.Id);
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
                    await sut.GetAccountAchievementsByIds(new int[0]);
                });
        }

        [Fact]
        [Trait("Feature",  "Accounts.Achievements")]
        [Trait("Category", "Integration")]
        public async Task GetAccountAchievementsByIds_ShouldReturnThoseAccountAchievements()
        {
            var services = new Container(ConfigurationManager.Instance.ApiKeyFull);
            var sut = services.Resolve<AccountAchievementService>();

            var ids = new[] { 1, 2, 3 };

            var actual = await sut.GetAccountAchievementsByIds(ids);

            Assert.Collection(actual, first => Assert.Equal(1, first.Id), second => Assert.Equal(2, second.Id), third => Assert.Equal(3, third.Id));
        }

        [Fact]
        [Trait("Feature",  "Accounts.Achievements")]
        [Trait("Category", "Integration")]
        public async Task GetAccountAchievementsByPage_WithInvalidPage_ShouldThrowArgumentException()
        {
            var services = new Container(ConfigurationManager.Instance.ApiKeyFull);
            var sut = services.Resolve<AccountAchievementService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetAccountAchievementsByPage(-1, 3));
        }

        [Fact]
        [Trait("Feature",  "Accounts.Achievements")]
        [Trait("Category", "Integration")]
        public async Task GetAccountAchievementsByPage_WithInvalidPageSize_ShouldThrowArgumentException()
        {
            var services = new Container(ConfigurationManager.Instance.ApiKeyFull);
            var sut = services.Resolve<AccountAchievementService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetAccountAchievementsByPage(1, -3));
        }

        [Fact]
        [Trait("Feature",  "Accounts.Achievements")]
        [Trait("Category", "Integration")]
        public async Task GetAccountAchievementsByPage_WithPage1AndPageSize3_ShouldReturnThatPage()
        {
            var services = new Container(ConfigurationManager.Instance.ApiKeyFull);
            var sut = services.Resolve<AccountAchievementService>();

            var actual = await sut.GetAccountAchievementsByPage(1, 3);

            Assert.Equal(3, actual.Count);
            Assert.Equal(3, actual.PageSize);
        }
    }
}
