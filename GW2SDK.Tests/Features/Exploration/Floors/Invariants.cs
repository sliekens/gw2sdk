using System.Drawing;
using GuildWars2.Exploration.Floors;

namespace GuildWars2.Tests.Features.Exploration.Floors;

public static class Invariants
{
    internal static void Has_texture_dimensions(this Floor actual) =>
        Assert.NotEqual(Size.Empty, actual.TextureDimensions);

    internal static void Has_regions(this Floor actual) => Assert.NotNull(actual.Regions);
}
