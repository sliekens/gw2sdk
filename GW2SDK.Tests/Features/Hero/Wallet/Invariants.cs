using GuildWars2.Hero.Wallet;

namespace GuildWars2.Tests.Features.Hero.Wallet;

internal static class Invariants
{
    internal static void Id_is_positive(this Currency actual) =>
        Assert.InRange(actual.Id, 1, int.MaxValue);

    internal static void Name_is_not_empty(this Currency actual) => Assert.NotEmpty(actual.Name);

    internal static void Description_is_not_empty(this Currency actual) =>
        Assert.NotEmpty(actual.Description);

    internal static void Order_is_positive(this Currency actual) =>
        Assert.InRange(actual.Order, 1, 1000);

    internal static void Icon_is_not_empty(this Currency actual) => Assert.NotEmpty(actual.Icon);
}
