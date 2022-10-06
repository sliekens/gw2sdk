using System.Linq;
using System.Text.Json;
using GW2SDK.Achievements;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements;

public class AchievementReaderTest : IClassFixture<AchievementFixture>
{
    public AchievementReaderTest(AchievementFixture fixture)
    {
        this.fixture = fixture;
    }

    private readonly AchievementFixture fixture;

    private static class AchievementFact
    {
        public static void Name_is_not_empty(Achievement actual) => Assert.NotEmpty(actual.Name);

        public static void Description_is_not_null(Achievement actual) =>
            Assert.NotNull(actual.Description);

        public static void Requirement_is_not_null(Achievement actual) =>
            Assert.NotNull(actual.Requirement);

        public static void LockedText_is_not_null(Achievement actual) =>
            Assert.NotNull(actual.LockedText);

        public static void Flags_is_not_empty(Achievement actual) => Assert.NotEmpty(actual.Flags);

        public static void Tiers_is_not_empty(Achievement actual) => Assert.NotEmpty(actual.Tiers);

        public static void Tiers_does_not_contain_null(Achievement actual) =>
            Assert.DoesNotContain(null, actual.Tiers);

        public static void Rewards_does_not_contain_null(Achievement actual)
        {
            if (actual.Rewards is not null)
            {
                Assert.DoesNotContain(null, actual.Rewards);
            }
        }

        public static void Bits_does_not_contain_null(Achievement actual)
        {
            if (actual.Bits is not null)
            {
                Assert.DoesNotContain(null, actual.Bits);
            }
        }

        public static void PointCap_is_negative_1_for_repeatable_achievements_without_points(
            Achievement actual
        )
        {
            if (actual.Flags.Contains(AchievementFlag.Repeatable)
                && actual.Tiers.All(tier => tier.Points == 0))
            {
                Assert.Equal(-1, actual.PointCap);
            }
        }
    }

    [Fact]
    public void Achievements_can_be_created_from_json() =>
        AssertEx.ForEach(
            fixture.Achievements,
            json =>
            {
                using var document = JsonDocument.Parse(json);

                var actual = document.RootElement.GetAchievement(MissingMemberBehavior.Error);

                AchievementFact.Name_is_not_empty(actual);
                AchievementFact.Description_is_not_null(actual);
                AchievementFact.Requirement_is_not_null(actual);
                AchievementFact.LockedText_is_not_null(actual);
                AchievementFact.Flags_is_not_empty(actual);
                AchievementFact.Tiers_is_not_empty(actual);
                AchievementFact.Tiers_does_not_contain_null(actual);
                AchievementFact.Rewards_does_not_contain_null(actual);
                AchievementFact.Bits_does_not_contain_null(actual);
                AchievementFact.PointCap_is_negative_1_for_repeatable_achievements_without_points(
                    actual
                );
            }
        );
}
