using GW2SDK.Tests.Features.Titles.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using GW2SDK.Titles;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Titles
{
    public class TitleTest : IClassFixture<TitlesFixture>
    {
        public TitleTest(TitlesFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly TitlesFixture _fixture;

        private readonly ITestOutputHelper _output;

        private static class TitleFact
        {
            public static void Id_is_positive(Title actual) => Assert.InRange(actual.Id, 1, int.MaxValue);

            public static void Name_is_not_empty(Title actual) => Assert.NotEmpty(actual.Name);

            public static void Can_be_unlocked_by_achievements_or_achievement_points(Title actual)
            {
                if (actual.AchievementPointsRequired.HasValue)
                {
                    Assert.InRange(actual.AchievementPointsRequired.Value, 1, 100000);
                }
                else
                {
                    Assert.NotEmpty(actual.Achievements!);
                }
            }
        }

        [Fact]
        [Trait("Feature",    "Titles")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Titles_can_be_created_from_json()
        {
            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(_output)
                .ThrowErrorOnMissingMember()
                .Build();

            AssertEx.ForEach(_fixture.Titles,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Title>(json, settings);
                    TitleFact.Id_is_positive(actual);
                    TitleFact.Name_is_not_empty(actual);
                    TitleFact.Can_be_unlocked_by_achievements_or_achievement_points(actual);
                });
        }
    }
}
