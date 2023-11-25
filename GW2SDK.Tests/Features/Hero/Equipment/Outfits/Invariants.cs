using GuildWars2.Hero.Equipment.Outfits;

namespace GuildWars2.Tests.Features.Hero.Equipment.Outfits;

internal static class Invariants
{
    internal static void Has_id(this Outfit actual) => Assert.True(actual.Id > 0);

    internal static void Has_name(this Outfit actual) => Assert.NotEmpty(actual.Name);

    internal static void Has_icon(this Outfit actual) => Assert.NotEmpty(actual.IconHref);

    internal static void Has_unlock_items(this Outfit actual) =>
        Assert.NotEmpty(actual.UnlockItems);
}
