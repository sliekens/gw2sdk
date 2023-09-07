using System.Linq;
using GuildWars2.Achievements;
using GuildWars2.Achievements.Categories;
using GuildWars2.Achievements.Groups;
using GuildWars2.Achievements.Titles;
using Xunit;

namespace GuildWars2.Tests.Features.Achievements;

internal static class Invariants
{
    internal static void Name_is_not_empty(this Achievement actual) => Assert.NotEmpty(actual.Name);

    internal static void Description_is_not_null(this Achievement actual) =>
        Assert.NotNull(actual.Description);

    internal static void Requirement_is_not_null(this Achievement actual) =>
        Assert.NotNull(actual.Requirement);

    internal static void LockedText_is_not_null(this Achievement actual) =>
        Assert.NotNull(actual.LockedText);

    internal static void Flags_is_not_empty(this Achievement actual) => Assert.NotEmpty(actual.Flags);

    internal static void Tiers_is_not_empty(this Achievement actual) => Assert.NotEmpty(actual.Tiers);

    internal static void Tiers_does_not_contain_null(this Achievement actual) =>
        Assert.DoesNotContain(null, actual.Tiers);

    internal static void Rewards_does_not_contain_null(this Achievement actual)
    {
        if (actual.Rewards is not null)
        {
            Assert.DoesNotContain(null, actual.Rewards);
        }
    }

    internal static void Bits_does_not_contain_null(this Achievement actual)
    {
        if (actual.Bits is not null)
        {
            Assert.DoesNotContain(null, actual.Bits);
        }
    }

    internal static void PointCap_is_negative_1_for_repeatable_achievements_without_points(
        this Achievement actual
    )
    {
        if (actual.Flags.Contains(AchievementFlag.Repeatable)
            && actual.Tiers.All(tier => tier.Points == 0))
        {
            Assert.Equal(-1, actual.PointCap);
        }
    }
    internal static void Id_is_positive(this AccountAchievement actual) =>
        Assert.InRange(actual.Id, 1, int.MaxValue);
    internal static void Id_is_positive(this AchievementCategory actual) =>
        Assert.InRange(actual.Id, 1, int.MaxValue);

    internal static void Order_is_not_negative(this AchievementCategory actual) =>
        Assert.InRange(actual.Order, 0, int.MaxValue);

    internal static void Order_is_not_negative(this AchievementGroup actual) =>
        Assert.InRange(actual.Order, 0, int.MaxValue);
    internal static void Id_is_positive(this Title actual) =>
        Assert.InRange(actual.Id, 1, int.MaxValue);

    internal static void Name_is_not_empty(this Title actual) => Assert.NotEmpty(actual.Name);

    internal static void Can_be_unlocked_by_achievements_or_achievement_points(this Title actual)
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
