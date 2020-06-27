using System.Linq;
using GW2SDK.Achievements;
using GW2SDK.Enums;
using GW2SDK.Tests.Features.Achievements.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Achievements
{
    [Collection(nameof(AchievementDbCollection))]
    public class AchievementTest
    {
        public AchievementTest(AchievementFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly AchievementFixture _fixture;

        private readonly ITestOutputHelper _output;

        private static class AchievementFact
        {
            public static void Name_is_not_empty(Achievement actual) => Assert.NotEmpty(actual.Name);

            public static void Description_is_not_null(Achievement actual) => Assert.NotNull(actual.Description);

            public static void Requirement_is_not_null(Achievement actual) => Assert.NotNull(actual.Requirement);

            public static void LockedText_is_not_null(Achievement actual) => Assert.NotNull(actual.LockedText);

            public static void Flags_is_not_empty(Achievement actual) => Assert.NotEmpty(actual.Flags);

            public static void Tiers_is_not_empty(Achievement actual) => Assert.NotEmpty(actual.Tiers);

            public static void Tiers_does_not_contain_null(Achievement actual) => Assert.DoesNotContain(null, actual.Tiers);

            public static void Rewards_does_not_contain_null(Achievement actual)
            {
                if (actual.Rewards is object)
                {
                    Assert.DoesNotContain(null, actual.Rewards);
                }
            }

            public static void Bits_does_not_contain_null(Achievement actual)
            {
                if (actual.Bits is object)
                {
                    Assert.DoesNotContain(null, actual.Bits);
                }
            }

            public static void PointCap_is_negative_1_for_repeatable_achievements_without_points(Achievement actual)
            {
                if (actual.Flags.Contains(AchievementFlag.Repeatable) && actual.Tiers.All(tier => tier.Points == 0))
                {
                    Assert.Equal(-1, actual.PointCap);
                }
            }
        }

        [Fact]
        [Trait("Feature",    "Achievements")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Achievements_can_be_created_from_json()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(_output)
                .ThrowErrorOnMissingMember()
                .Build();

            AssertEx.ForEach(_fixture.Db.Achievements,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Achievement>(json, settings);

                    AchievementFact.Name_is_not_empty(actual);
                    AchievementFact.Description_is_not_null(actual);
                    AchievementFact.Requirement_is_not_null(actual);
                    AchievementFact.LockedText_is_not_null(actual);
                    AchievementFact.Flags_is_not_empty(actual);
                    AchievementFact.Tiers_is_not_empty(actual);
                    AchievementFact.Tiers_does_not_contain_null(actual);
                    AchievementFact.Rewards_does_not_contain_null(actual);
                    AchievementFact.Bits_does_not_contain_null(actual);
                    AchievementFact.PointCap_is_negative_1_for_repeatable_achievements_without_points(actual);
                });
        }
    }
}
