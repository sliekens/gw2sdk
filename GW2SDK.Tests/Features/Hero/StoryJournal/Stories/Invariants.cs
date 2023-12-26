using GuildWars2.Hero.StoryJournal.Stories;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.Stories;

internal static class Invariants
{
    internal static void Has_id(this StoryStep actual) => Assert.True(actual.Id > 0);

    internal static void Has_name(this StoryStep actual) => Assert.NotEmpty(actual.Name);

    internal static void Has_level(this StoryStep actual) => Assert.True(actual.Level > 0);

    internal static void Has_story(this StoryStep actual) => Assert.True(actual.StoryId > 0);

    internal static void Has_goals(this StoryStep actual) => Assert.NotEmpty(actual.Objectives);
}
