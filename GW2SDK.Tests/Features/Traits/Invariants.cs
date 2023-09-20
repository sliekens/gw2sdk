using GuildWars2.Traits;

namespace GuildWars2.Tests.Features.Traits;

internal static class Invariants
{
    internal static void Id_is_positive(this Trait actual) =>
        Assert.InRange(actual.Id, 1, int.MaxValue);
}
