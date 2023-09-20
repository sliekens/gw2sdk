using GuildWars2.Wvw.Abilities;

namespace GuildWars2.Tests.Features.Wvw.Abilities;

internal static class Invariants
{
    internal static void Has_id(this Ability actual) => Assert.True(actual.Id > 0);

    internal static void Has_name(this Ability actual) => Assert.NotEmpty(actual.Name);

    internal static void Has_description(this Ability actual) =>
        Assert.NotEmpty(actual.Description);

    internal static void Has_icon(this Ability actual) => Assert.NotEmpty(actual.Icon);

    internal static void Has_ranks(this Ability actual) => Assert.NotEmpty(actual.Ranks);
}
