using GuildWars2.Hero.Equipment.Miniatures;

namespace GuildWars2.Tests.Features.Hero.Equipment.Miniatures;

internal static class Invariants
{
    internal static void Has_id(this Miniature actual) => Assert.True(actual.Id > 0);

    internal static void Has_name(this Miniature actual) => Assert.NotEmpty(actual.Name);

    internal static void Has_icon(this Miniature actual) => Assert.NotEmpty(actual.IconHref);

    internal static void Has_order(this Miniature actual) => Assert.True(actual.Order >= 0);

    internal static void Has_item_id(this Miniature actual) => Assert.True(actual.ItemId >= 0);
}
