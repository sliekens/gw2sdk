using GuildWars2.Hero.Achievements;
using GuildWars2.Hero.Achievements.Categories;
using GuildWars2.Hero.Achievements.Groups;
using GuildWars2.Hero.Achievements.Titles;

namespace GuildWars2.Tests.Features.Hero.Achievements;

internal static class Invariants
{
    internal static void Has_id(this AccountAchievement actual) =>
        Assert.InRange(actual.Id, 1, int.MaxValue);

    internal static void Has_id(this AchievementCategory actual) =>
        Assert.InRange(actual.Id, 1, int.MaxValue);

    internal static void Has_name(this AchievementCategory actual) => Assert.NotEmpty(actual.Name);

    internal static void Has_description(this AchievementCategory actual) =>
        Assert.NotNull(actual.Description);

    internal static void Has_icon(this AchievementCategory actual) =>
        Assert.NotEmpty(actual.IconHref);

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
