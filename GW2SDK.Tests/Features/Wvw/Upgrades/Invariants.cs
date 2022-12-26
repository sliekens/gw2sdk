using GuildWars2.Wvw.Upgrades;
using Xunit;

namespace GuildWars2.Tests.Features.Wvw.Upgrades;

internal static class Invariants
{
    internal static void Has_id(this Upgrade actual) => Assert.True(actual.Id > 0);

    internal static void Has_tiers(this Upgrade actual) => Assert.NotEmpty(actual.Tiers);
}
