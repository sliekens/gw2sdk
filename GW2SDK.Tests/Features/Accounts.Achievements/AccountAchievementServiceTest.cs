using System;
using System.Linq;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Accounts.Achievements;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Shared;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Accounts.Achievements
{
    public class AccountAchievementServiceTest
    {
        public AccountAchievementServiceTest(ITestOutputHelper output)
        {
            _output = output;
        }

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature",  "Accounts.Achievements")]
        [Trait("Category", "Integration")]
        public async Task GetAccountAchievementById_ShouldReturnAccountAchievement()
        {
            var http = HttpClientFactory.CreateDefault();
            http.UseAccessToken(ConfigurationManager.Instance.ApiKeyFull);

            var sut = new AccountAchievementService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            // Randomly chosen
            var achievementId = 1;

            var actual = await sut.GetAccountAchievementById(achievementId, settings);

            Assert.IsType<AccountAchievement>(actual);
        }

        [Fact]
        [Trait("Feature",  "Accounts.Achievements")]
        [Trait("Category", "Integration")]
        public async Task GetAccountAchievementsByIds_ShouldReturnExpectedRange()
        {
            var http = HttpClientFactory.CreateDefault();
            http.UseAccessToken(ConfigurationManager.Instance.ApiKeyFull);

            var sut = new AccountAchievementService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            // Randomly chosen
            var ids = new[] { 1, 2, 3, 4, 5 };

            var actual = await sut.GetAccountAchievementsByIds(ids, settings);

            Assert.Equal(ids, actual.Select(accountAchievement => accountAchievement.Id));
        }

        [Fact]
        [Trait("Feature",  "Accounts.Achievements")]
        [Trait("Category", "Unit")]
        public async Task GetAccountAchievementsByIds_WithIdsNull_ShouldThrowArgumentNullException()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new AccountAchievementService(http);

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
            var http = HttpClientFactory.CreateDefault();

            var sut = new AccountAchievementService(http);

            await Assert.ThrowsAsync<ArgumentException>("achievementIds",
                async () =>
                {
                    await sut.GetAccountAchievementsByIds(Enumerable.Empty<int>().ToList());
                });
        }

        [Fact]
        [Trait("Feature",  "Accounts.Achievements")]
        [Trait("Category", "Integration")]
        public async Task GetAllAccountAchievements_ShouldReturnAllAccountAchievements()
        {
            var http = HttpClientFactory.CreateDefault();
            http.UseAccessToken(ConfigurationManager.Instance.ApiKeyFull);

            var sut = new AccountAchievementService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = await sut.GetAllAccountAchievements(settings);

            Assert.Equal(actual.Count, actual.ResultTotal);
            Assert.Equal(actual.Count, actual.ResultCount);
        }

        [Fact]
        [Trait("Feature",  "Accounts.Achievements")]
        [Trait("Category", "Integration")]
        public async Task GetAccountAchievementsByPage_ShouldThrowArgumentExceptionForInvalidPage()
        {
            var http = HttpClientFactory.CreateDefault();
            http.UseAccessToken(ConfigurationManager.Instance.ApiKeyFull);

            var sut = new AccountAchievementService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetAccountAchievementsByPage(-1, 50, settings));
        }

        [Fact]
        [Trait("Feature",  "Accounts.Achievements")]
        [Trait("Category", "Integration")]
        public async Task GetAccountAchievementsByPage_ShouldReturnExpectedLimit()
        {
            var http = HttpClientFactory.CreateDefault();
            http.UseAccessToken(ConfigurationManager.Instance.ApiKeyFull);

            var sut = new AccountAchievementService(http);

            var limit = 50;

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = await sut.GetAccountAchievementsByPage(0, limit, settings);

            Assert.InRange(actual.Count, 0, limit);
        }
    }
}
