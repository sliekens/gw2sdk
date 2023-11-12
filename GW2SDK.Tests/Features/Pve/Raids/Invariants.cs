using GuildWars2.Pve.Raids;

namespace GuildWars2.Tests.Features.Pve.Raids;

internal static class Invariants
{
    internal static void Has_id(this Raid actual) => Assert.NotEmpty(actual.Id);
}
