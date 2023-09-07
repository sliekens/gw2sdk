using GuildWars2.Colors;
using Xunit;

namespace GuildWars2.Tests.Features.Colors;

internal static class Invariants
{
    internal static void Base_rgb_contains_red_green_blue(this Dye actual) =>
        Assert.False(actual.BaseRgb.IsEmpty);
}
