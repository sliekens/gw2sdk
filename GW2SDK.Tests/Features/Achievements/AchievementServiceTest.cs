using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GW2SDK.Features.Achievements;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Shared;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Achievements
{
    public class AchievementServiceTest
    {
        public AchievementServiceTest(ITestOutputHelper output)
        {
            _output = output;
        }

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public async Task GetAchievementIds_ShouldReturnAllAchievementIds()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new AchievementService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = await sut.GetAchievementIndex(settings);

            Assert.NotEmpty(actual);
            Assert.Equal(actual.ResultCount, actual.Count);
            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public async Task GetAchievementsByIds_ShouldReturnRequestedAchievement()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new AchievementService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievementId = 1;

            var actual = await sut.GetAchievementById(achievementId, settings);

            Assert.Equal(achievementId, actual.Id);
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public async Task GetAchievementsByIds_ShouldReturnRequestedAchievements()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new AchievementService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var achievementIds = new List<int> { 1, 2, 3 };

            var actual = await sut.GetAchievementsByIds(achievementIds, settings);

            Assert.NotEmpty(actual);
            Assert.Collection(actual,
                achievement => Assert.Equal(1, achievement.Id),
                achievement => Assert.Equal(2, achievement.Id),
                achievement => Assert.Equal(3, achievement.Id));
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Unit")]
        public async Task GetAchievementsByIds_WithIdsNull_ShouldThrowArgumentNullException()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new AchievementService(http);

            await Assert.ThrowsAsync<ArgumentNullException>("achievementIds",
                async () =>
                {
                    await sut.GetAchievementsByIds(null);
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Unit")]
        public async Task GetAchievementsByIds_WithIdsEmpty_ShouldThrowArgumentException()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new AchievementService(http);

            await Assert.ThrowsAsync<ArgumentException>("achievementIds",
                async () =>
                {
                    await sut.GetAchievementsByIds(Enumerable.Empty<int>().ToList());
                });
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public async Task GetWorldsByPage_ShouldReturnThePage()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new AchievementService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = await sut.GetAchievementsByPage(0, 200, settings);

            Assert.NotEmpty(actual);
            Assert.Equal(actual.Count, actual.ResultCount);
            Assert.Equal(200,          actual.PageSize);
        }
    }
}
