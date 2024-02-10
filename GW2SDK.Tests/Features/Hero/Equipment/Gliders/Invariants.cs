using GuildWars2.Hero.Equipment.Gliders;

namespace GuildWars2.Tests.Features.Hero.Equipment.Gliders;

internal static class Invariants
{
    internal static void Has_id(this GliderSkin actual) => Assert.True(actual.Id > 0);

    internal static void Has_unlock_items(this GliderSkin actual) => Assert.NotNull(actual.UnlockItemIds);

    internal static void Has_order(this GliderSkin actual) => Assert.True(actual.Order >= 0);

    internal static void Has_icon(this GliderSkin actual) => Assert.NotEmpty(actual.IconHref);

    internal static void Has_name(this GliderSkin actual) => Assert.NotEmpty(actual.Name);

    internal static void Has_description(this GliderSkin actual) => Assert.NotNull(actual.Description);

    internal static void Has_default_dyes(this GliderSkin actual) => Assert.NotNull(actual.DefaultDyes);
}
