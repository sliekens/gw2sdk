using GuildWars2.Gliders;

namespace GuildWars2.Tests.Features.Gliders;

internal static class Invariants
{
    internal static void Has_id(this Glider actual) => Assert.True(actual.Id > 0);

    internal static void Has_unlock_items(this Glider actual) => Assert.NotNull(actual.UnlockItems);

    internal static void Has_order(this Glider actual) => Assert.True(actual.Order >= 0);

    internal static void Has_icon(this Glider actual) => Assert.NotEmpty(actual.Icon);

    internal static void Has_name(this Glider actual) => Assert.NotEmpty(actual.Name);

    internal static void Has_description(this Glider actual) => Assert.NotNull(actual.Description);

    internal static void Has_default_dyes(this Glider actual) => Assert.NotNull(actual.DefaultDyes);
}
