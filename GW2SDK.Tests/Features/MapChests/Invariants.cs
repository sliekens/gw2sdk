using GuildWars2.MapChests;

namespace GuildWars2.Tests.Features.MapChests;

public static class Invariants
{
    internal static void Has_id(this MapChest actual) => Assert.NotEmpty(actual.Id);
}
