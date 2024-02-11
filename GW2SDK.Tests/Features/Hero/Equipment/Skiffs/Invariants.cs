using GuildWars2.Hero.Equipment.Skiffs;

namespace GuildWars2.Tests.Features.Hero.Equipment.Skiffs;

internal static class Invariants
{
    internal static void Has_id(this SkiffSkin actual) => Assert.True(actual.Id > 0);

    internal static void Has_dye_slots(this SkiffSkin actual) => Assert.NotNull(actual.DyeSlots);

    internal static void Has_icon(this SkiffSkin actual) => Assert.NotEmpty(actual.IconHref);

    internal static void Has_name(this SkiffSkin actual) => Assert.NotEmpty(actual.Name);
}
