using System.Collections.Generic;
using GW2SDK.Extensions;
using GW2SDK.Features.Accounts.Achievements;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Features.Accounts.Achievements.Fixtures;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Accounts.Achievements
{
    public class AchievementTest : IClassFixture<AchievementFixture>
    {
        public AchievementTest(AchievementFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly AchievementFixture _fixture;

        private readonly ITestOutputHelper _output;

        private List<Achievement> CreateSut(JsonSerializerSettings jsonSerializerSettings)
        {
            _output.WriteLine(_fixture.AchievementJsonArray);
            var sut = new List<Achievement>();
            JsonConvert.PopulateObject(_fixture.AchievementJsonArray, sut, jsonSerializerSettings);
            return sut;
        }

        [Fact]
        [Trait("Feature", "Accounts.Achievements")]
        [Trait("Category", "Integration")]
        [Trait("Importance", "Critical")]
        public void Achievement_ShouldHaveNoMissingMembers()
        {
            _ = CreateSut(Json.DefaultJsonSerializerSettings.WithMissingMemberHandling(MissingMemberHandling.Error));
        }
    }
}
