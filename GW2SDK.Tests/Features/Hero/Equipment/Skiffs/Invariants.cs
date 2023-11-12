using GuildWars2.Hero.Equipment.Skiffs;

namespace GuildWars2.Tests.Features.Hero.Equipment.Skiffs;

internal static class Invariants
{
    internal static void Has_id(this Skiff actual) => Assert.True(actual.Id > 0);

    internal static void Has_dye_slots(this Skiff actual) => Assert.NotNull(actual.DyeSlots);

    internal static void Has_icon(this Skiff actual) => Assert.NotEmpty(actual.IconHref);

    internal static void Has_name(this Skiff actual) => Assert.NotEmpty(actual.Name);
}
