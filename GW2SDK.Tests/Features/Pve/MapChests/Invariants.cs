using GuildWars2.Pve.MapChests;

namespace GuildWars2.Tests.Features.Pve.MapChests;

public static class Invariants
{
    internal static void Has_id(this MapChest actual) => Assert.NotEmpty(actual.Id);
}
