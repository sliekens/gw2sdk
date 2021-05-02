using System.Text.Json;
using GW2SDK.Json;
using GW2SDK.Tests.Features.Titles.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using GW2SDK.Titles;
using Xunit;

namespace GW2SDK.Tests.Features.Titles
{
    public class TitleReaderTest : IClassFixture<TitlesFixture>
    {
        public TitleReaderTest(TitlesFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly TitlesFixture _fixture;

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
            var sut = new TitleReader();

            AssertEx.ForEach(_fixture.Titles,
                json =>
                {
                    using var document = JsonDocument.Parse(json);
                    var actual = sut.Read(document.RootElement, MissingMemberBehavior.Error);
                    TitleFact.Id_is_positive(actual);
                    TitleFact.Name_is_not_empty(actual);
                    TitleFact.Can_be_unlocked_by_achievements_or_achievement_points(actual);
                });
        }
    }
}
