using GuildWars2.Hero.Dyes;

namespace GuildWars2.Tests.Features.Hero.Dyes;

internal static class Invariants
{
    internal static void Base_rgb_contains_red_green_blue(this Dye actual) =>
        Assert.False(actual.BaseRgb.IsEmpty);
}
