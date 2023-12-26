using GuildWars2.Hero.StoryJournal.BackgroundStories;
using GuildWars2.Hero.StoryJournal.Stories;

namespace GuildWars2.Tests.Features.Hero.StoryJournal;

internal static class Invariants
{
    internal static void Has_id(this Story actual) => Assert.True(actual.Id > 0);

    internal static void Has_id(this Storyline actual) => Assert.NotEmpty(actual.Id);

    internal static void Id_is_positive(this BackgroundStoryQuestion actual) =>
        Assert.InRange(actual.Id, 1, int.MaxValue);

    internal static void Title_is_not_null(this BackgroundStoryQuestion actual) =>
        Assert.NotNull(actual.Title);

    internal static void Description_is_not_empty(this BackgroundStoryQuestion actual) =>
        Assert.NotEmpty(actual.Description);

    internal static void Has_3_to_8_answers(this BackgroundStoryQuestion actual) =>
        Assert.InRange(actual.AnswerIds.Count, 3, 8);

    internal static void Id_is_not_empty(this BackgroundStoryAnswer actual) => Assert.NotEmpty(actual.Id);

    internal static void Title_is_not_null(this BackgroundStoryAnswer actual) =>
        Assert.NotNull(actual.Title);

    internal static void Description_is_not_empty(this BackgroundStoryAnswer actual) =>
        Assert.NotEmpty(actual.Description);

    internal static void Journal_is_not_empty(this BackgroundStoryAnswer actual) =>
        Assert.NotEmpty(actual.Journal);

    internal static void Has_a_question(this BackgroundStoryAnswer actual) =>
        Assert.InRange(actual.QuestionId, 1, 999);
}
