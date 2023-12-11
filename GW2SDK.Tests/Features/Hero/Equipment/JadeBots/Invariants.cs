using GuildWars2.Hero.Equipment.JadeBots;

namespace GuildWars2.Tests.Features.Hero.Equipment.JadeBots;

internal static class Invariants
{
    internal static void Has_id(this JadeBot actual) => Assert.True(actual.Id > 0);

    internal static void Has_name(this JadeBot actual) => Assert.NotEmpty(actual.Name);

    internal static void Has_description(this JadeBot actual) =>
        Assert.NotEmpty(actual.Description);

    internal static void Has_unlock_item(this JadeBot actual) =>
        Assert.True(actual.UnlockItemId > 0);
}
