using System.Text.Json;
using GW2SDK.Achievements.Groups;
using GW2SDK.Json;
using GW2SDK.Tests.Features.Achievements.Groups.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements.Groups
{
    public class AchievementGroupReaderTest : IClassFixture<AchievementGroupFixture>
    {
        public AchievementGroupReaderTest(AchievementGroupFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly AchievementGroupFixture _fixture;

        private static class AchievementGroupFact
        {
            public static void Order_is_not_negative(AchievementGroup actual) => Assert.InRange(actual.Order, 0, int.MaxValue);
        }

        [Fact]
        [Trait("Feature",    "Achievements.Groups")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Achievement_groups_can_be_created_from_json()
        {
            var sut = new AchievementGroupReader();

            AssertEx.ForEach(_fixture.AchievementGroups,
                json =>
                {
                    using var document = JsonDocument.Parse(json);

                    var actual = sut.Read(document.RootElement, MissingMemberBehavior.Error);

                    AchievementGroupFact.Order_is_not_negative(actual);
                });
        }
    }
}
