using GuildWars2.Hero.StoryJournal.Stories;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.Stories;

internal static class Invariants
{
    internal static void Has_id(this Quest actual) => Assert.True(actual.Id > 0);

    internal static void Has_name(this Quest actual) => Assert.NotEmpty(actual.Name);

    internal static void Has_level(this Quest actual) => Assert.True(actual.Level > 0);

    internal static void Has_story(this Quest actual) => Assert.True(actual.Story > 0);

    internal static void Has_goals(this Quest actual) => Assert.NotEmpty(actual.Goals);
}
