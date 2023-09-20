using GuildWars2.Accounts;

namespace GuildWars2.Tests.Features.Accounts;

public static class Invariants
{
    public static void Name_is_never_empty(this AccountSummary actual) =>
        Assert.NotEmpty(actual.DisplayName);

    public static void Access_is_never_empty(this AccountSummary actual) =>
        Assert.NotEmpty(actual.Access);

    public static void Access_is_never_none(this AccountSummary actual) =>
        Assert.DoesNotContain(ProductName.None, actual.Access);

    public static void GuildLeader_requires_guilds_scope(this AccountSummary actual) =>
        Assert.Null(actual.LeaderOfGuildIds);

    public static void Age_is_never_zero(this AccountSummary actual) =>
        Assert.NotEqual(TimeSpan.Zero, actual.Age);

    public static void FractalLevel_requires_progression_scope(this AccountSummary actual) =>
        Assert.Null(actual.FractalLevel);

    public static void DailyAp_requires_progression_scope(this AccountSummary actual) =>
        Assert.Null(actual.DailyAchievementPoints);

    public static void MonthlyAp_requires_progression_scope(this AccountSummary actual) =>
        Assert.Null(actual.MonthlyAchievementPoints);

    public static void WvwRank_requires_progression_scope(this AccountSummary actual) =>
        Assert.Null(actual.WvwRank);

    public static void GuildLeader_is_included_by_guilds_scope(this AccountSummary actual) =>
        Assert.NotNull(actual.LeaderOfGuildIds);

    public static void Created_ShouldNotBeDefaultValue(this AccountSummary actual) =>
        Assert.NotEqual(default, actual.Created);

    public static void FractalLevel_is_included_by_progression_scope(this AccountSummary actual) =>
        Assert.NotNull(actual.FractalLevel);

    public static void DailyAp_is_included_by_progression_scope(this AccountSummary actual) =>
        Assert.NotNull(actual.DailyAchievementPoints);

    public static void MonthlyAp_is_included_by_progression_scope(this AccountSummary actual) =>
        Assert.NotNull(actual.MonthlyAchievementPoints);

    public static void WvwRank_is_included_by_progression_scope(this AccountSummary actual) =>
        Assert.NotNull(actual.WvwRank);
}
