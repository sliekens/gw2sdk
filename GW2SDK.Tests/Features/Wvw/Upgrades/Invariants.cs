using GuildWars2.Wvw.Upgrades;

namespace GuildWars2.Tests.Features.Wvw.Upgrades;

internal static class Invariants
{
    internal static void Has_id(this ObjectiveUpgrade actual) => Assert.True(actual.Id > 0);

    internal static void Has_tiers(this ObjectiveUpgrade actual) => Assert.NotEmpty(actual.Tiers);
}
