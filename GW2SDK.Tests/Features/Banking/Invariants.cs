using GuildWars2.Banking;
using GuildWars2.Inventories;
using Xunit;

namespace GuildWars2.Tests.Features.Banking;

internal static class Invariants
{
    internal static void Has_id(this MaterialCategory actual) => Assert.True(actual.Id > 0);

    internal static void Has_name(this MaterialCategory actual) => Assert.NotEmpty(actual.Name);

    internal static void Has_items(this MaterialCategory actual) => Assert.NotEmpty(actual.Items);

    internal static void Has_order(this MaterialCategory actual) => Assert.True(actual.Order >= 0);

    public static void Not_empty(this Bank actual) => Assert.NotEmpty(actual);

    public static void Has_multiple_of_30_slots(this Bank actual) =>
        Assert.Equal(0, actual.Count % 30);

    public static void Has_id(this ItemSlot actual) => Assert.True(actual.Id > 0);

    public static void Has_count(this ItemSlot actual) => Assert.True(actual.Count > 0);
}
