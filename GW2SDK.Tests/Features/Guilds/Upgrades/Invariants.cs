using GuildWars2.Guilds.Upgrades;

namespace GuildWars2.Tests.Features.Guilds.Upgrades;

internal static class Invariants
{
    internal static void Has_id(this GuildUpgrade actual) => Assert.True(actual.Id > 0);

    internal static void Has_name(this GuildUpgrade actual) => Assert.NotNull(actual.Name);

    internal static void Has_description(this GuildUpgrade actual) =>
        Assert.NotNull(actual.Description);

    internal static void Has_icon(this GuildUpgrade actual) => Assert.NotEmpty(actual.IconHref);

    internal static void Has_costs(this GuildUpgrade actual) => Assert.NotNull(actual.Costs);

    internal static void Has_MaxItems(this BankBag actual) => Assert.True(actual.MaxItems > 0);

    internal static void Has_MaxCoins(this BankBag actual) => Assert.True(actual.MaxCoins > 0);
}
