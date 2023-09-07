using GuildWars2.Commerce.Prices;
using Xunit;

namespace GuildWars2.Tests.Features.Commerce.Prices;

internal static class Invariants
{
    internal static void Id_is_positive(this ItemPrice actual) =>
        Assert.InRange(actual.Id, 1, int.MaxValue);

    internal static void Best_ask_is_greater_than_best_bid(this ItemPrice actual)
    {
        if (actual.TotalDemand > 0 && actual.TotalSupply > 0)
        {
            Assert.True(actual.BestAsk - actual.BestBid > 0);
        }
    }
}
