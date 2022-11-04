using GW2SDK.MapChests;
using Xunit;

namespace GW2SDK.Tests.Features.MapChests;

public static class Invariants
{
    internal static void Has_id(this MapChest actual) => Assert.NotEmpty(actual.Id);
}
