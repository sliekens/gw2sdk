using GuildWars2.MapChests;
using Xunit;

namespace GuildWars2.Tests.Features.MapChests;

public static class Invariants
{
    internal static void Has_id(this MapChest actual) => Assert.NotEmpty(actual.Id);
}
