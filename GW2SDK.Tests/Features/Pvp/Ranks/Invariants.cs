using GuildWars2.Pvp.Ranks;

namespace GuildWars2.Tests.Features.Pvp.Ranks;

internal static class Invariants
{
    internal static void Has_id(this Rank actual) => Assert.True(actual.Id > 0);

    internal static void Has_name(this Rank actual) => Assert.NotEmpty(actual.Name);

    internal static void Has_icon(this Rank actual) => Assert.NotEmpty(actual.IconHref);

    internal static void Has_levels(this Rank actual) => Assert.NotEmpty(actual.Levels);
}
