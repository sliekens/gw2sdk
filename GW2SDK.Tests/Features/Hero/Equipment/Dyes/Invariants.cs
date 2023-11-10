using GuildWars2.Hero.Equipment.Dyes;

namespace GuildWars2.Tests.Features.Hero.Equipment.Dyes;

internal static class Invariants
{
    internal static void Base_rgb_contains_red_green_blue(this Dye actual) =>
        Assert.False(actual.BaseRgb.IsEmpty);
}
