using GuildWars2.Specializations;

namespace GuildWars2.Tests.Features.Specializations;

internal static class Invariants
{
    internal static void Id_is_positive(this Specialization actual) =>
        Assert.InRange(actual.Id, 1, int.MaxValue);

    internal static void Name_is_not_empty(this Specialization actual) => Assert.NotEmpty(actual.Name);

    internal static void It_has_minor_traits(this Specialization actual) =>
        Assert.NotEmpty(actual.MinorTraits);

    internal static void It_has_major_traits(this Specialization actual) =>
        Assert.NotEmpty(actual.MajorTraits);

    internal static void Icon_is_not_empty(this Specialization actual) => Assert.NotEmpty(actual.Icon);

    internal static void Background_is_not_empty(this Specialization actual) =>
        Assert.NotEmpty(actual.Icon);

    internal static void Profession_icon_is_not_null(this Specialization actual) =>
        Assert.NotNull(actual.ProfessionIcon);

    internal static void Big_profession_icon_is_not_null(this Specialization actual) =>
        Assert.NotNull(actual.ProfessionIconBig);
}
