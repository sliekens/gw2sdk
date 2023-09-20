using GuildWars2.Achievements;
using GuildWars2.Achievements.Categories;
using GuildWars2.Achievements.Groups;
using GuildWars2.Achievements.Titles;

namespace GuildWars2.Tests.Features.Achievements;

internal static class Invariants
{
    internal static void Has_id(this Achievement actual) => Assert.True(actual.Id > 0);

    internal static void Has_name(this Achievement actual) => Assert.NotEmpty(actual.Name);

    internal static void Has_description(this Achievement actual) =>
        Assert.NotNull(actual.Description);

    internal static void Has_requirement(this Achievement actual) =>
        Assert.NotNull(actual.Requirement);

    internal static void Has_LockedText(this Achievement actual) =>
        Assert.NotNull(actual.LockedText);

    internal static void Has_flags(this Achievement actual) => Assert.NotEmpty(actual.Flags);

    internal static void Has_tiers(this Achievement actual) => Assert.NotEmpty(actual.Tiers);

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

    internal static void Has_id(this AccountAchievement actual) =>
        Assert.InRange(actual.Id, 1, int.MaxValue);

    internal static void Has_id(this AchievementCategory actual) =>
        Assert.InRange(actual.Id, 1, int.MaxValue);

    internal static void Has_name(this AchievementCategory actual) => Assert.NotEmpty(actual.Name);

    internal static void Has_description(this AchievementCategory actual) =>
        Assert.NotNull(actual.Description);

    internal static void Has_icon(this AchievementCategory actual) => Assert.NotEmpty(actual.Icon);

    internal static void Has_achievements(this AchievementCategory actual) =>
        Assert.NotNull(actual.Achievements);

    internal static void Has_order(this AchievementCategory actual) =>
        Assert.InRange(actual.Order, 0, int.MaxValue);

    internal static void Has_id(this AchievementGroup actual) => Assert.NotEmpty(actual.Id);

    internal static void Has_name(this AchievementGroup actual) => Assert.NotEmpty(actual.Name);

    internal static void Has_description(this AchievementGroup actual) =>
        Assert.NotNull(actual.Description);

    internal static void Has_categories(this AchievementGroup actual) =>
        Assert.NotEmpty(actual.Categories);

    internal static void Has_order(this AchievementGroup actual) =>
        Assert.InRange(actual.Order, 0, int.MaxValue);

    internal static void Has_id(this Title actual) => Assert.InRange(actual.Id, 1, int.MaxValue);

    internal static void Has_name(this Title actual) => Assert.NotEmpty(actual.Name);

    internal static void Can_be_unlocked_by_achievements(this Title actual)
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
