using GuildWars2.Commerce.Listings;

namespace GuildWars2.Tests.Features.Commerce.Listings;

internal static class Invariants
{
    internal static void Id_is_positive(this OrderBook actual) =>
        Assert.InRange(actual.Id, 1, int.MaxValue);
}
