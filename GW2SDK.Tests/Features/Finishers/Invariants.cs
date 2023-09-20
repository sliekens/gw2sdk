using GuildWars2.Finishers;

namespace GuildWars2.Tests.Features.Finishers;

internal static class Invariants
{
    internal static void Has_id(this Finisher actual) => Assert.True(actual.Id > 0);

    internal static void Has_unlock_details(this Finisher actual) =>
        Assert.NotNull(actual.UnlockDetails);

    internal static void Has_unlock_items(this Finisher actual) =>
        Assert.NotNull(actual.UnlockItems);

    internal static void Has_order(this Finisher actual) => Assert.True(actual.Order >= 0);

    internal static void Has_icon(this Finisher actual) => Assert.NotEmpty(actual.Icon);

    internal static void Has_name(this Finisher actual) => Assert.NotEmpty(actual.Name);
}
