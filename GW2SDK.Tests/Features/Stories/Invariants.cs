using GuildWars2.Stories;
using Xunit;

namespace GuildWars2.Tests.Features.Stories;

internal static class Invariants
{
    internal static void Has_id(this Story actual) => Assert.True(actual.Id > 0);

    internal static void Has_id(this Season actual) => Assert.NotEmpty(actual.Id);

    internal static void Id_is_positive(this BackstoryQuestion actual) =>
        Assert.InRange(actual.Id, 1, int.MaxValue);

    internal static void Title_is_not_null(this BackstoryQuestion actual) =>
        Assert.NotNull(actual.Title);

    internal static void Description_is_not_empty(this BackstoryQuestion actual) =>
        Assert.NotEmpty(actual.Description);

    internal static void Has_3_to_8_answers(this BackstoryQuestion actual) =>
        Assert.InRange(actual.Answers.Count, 3, 8);

    internal static void Id_is_not_empty(this BackstoryAnswer actual) => Assert.NotEmpty(actual.Id);

    internal static void Title_is_not_null(this BackstoryAnswer actual) =>
        Assert.NotNull(actual.Title);

    internal static void Description_is_not_empty(this BackstoryAnswer actual) =>
        Assert.NotEmpty(actual.Description);

    internal static void Journal_is_not_empty(this BackstoryAnswer actual) =>
        Assert.NotEmpty(actual.Journal);

    internal static void Has_a_question(this BackstoryAnswer actual) =>
        Assert.InRange(actual.Question, 1, 999);
}
