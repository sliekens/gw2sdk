using GuildWars2.Hero.Equipment.Wardrobe;

namespace GuildWars2.Tests.Features.Hero.Equipment.Wardrobe;

internal static class Invariants
{
    internal static void Has_id(this Skin actual) => Assert.InRange(actual.Id, 1, int.MaxValue);

    internal static void Has_races(this Skin actual) => Assert.NotEmpty(actual.Races);
}
