using GuildWars2.Hero.Masteries;

namespace GuildWars2.Tests.Features.Hero.Masteries;

internal static class Invariants
{
    internal static void Id_is_positive(this MasteryTrack actual) =>
        Assert.InRange(actual.Id, 1, int.MaxValue);

    internal static void Name_is_not_empty(this MasteryTrack actual) => Assert.NotEmpty(actual.Name);

    internal static void Requirement_is_not_null(this MasteryTrack actual) =>
        Assert.NotNull(actual.Requirement);

    internal static void Order_is_not_negative(this MasteryTrack actual) =>
        Assert.InRange(actual.Order, 0, int.MaxValue);

    internal static void Background_is_not_empty(this MasteryTrack actual) =>
        Assert.NotEmpty(actual.BackgroundHref);

    internal static void Region_is_known(this MasteryTrack actual) =>
        Assert.NotEqual(MasteryRegionName.Unknown, actual.Region);

    internal static void Name_is_not_empty(this Mastery actual) =>
        Assert.NotEmpty(actual.Name);

    internal static void Description_is_not_empty(this Mastery actual) =>
        Assert.NotEmpty(actual.Description);

    internal static void Instruction_is_not_empty(this Mastery actual) =>
        Assert.NotEmpty(actual.Instruction);

    internal static void Icon_is_not_empty(this Mastery actual) =>
        Assert.NotEmpty(actual.IconHref);

    internal static void Costs_points(this Mastery actual) =>
        Assert.InRange(actual.PointCost, 1, int.MaxValue);

    internal static void Costs_experience(this Mastery actual) =>
        Assert.InRange(actual.ExperienceCost, 1, int.MaxValue);
}
