using GuildWars2.Hero.Wardrobe;

namespace GuildWars2.Tests.Features.Hero.Wardrobe;

internal static class Invariants
{
    internal static void Has_id(this Skin actual) =>
        Assert.InRange(actual.Id, 1, int.MaxValue);
}
