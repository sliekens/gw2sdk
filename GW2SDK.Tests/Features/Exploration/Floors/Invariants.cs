using System.Drawing;
using GW2SDK.Exploration.Floors;
using Xunit;

namespace GW2SDK.Tests.Features.Exploration.Floors;

public static class Invariants
{
    internal static void Has_texture_dimensions(this Floor actual) =>
        Assert.NotEqual(Size.Empty, actual.TextureDimensions);

    internal static void Has_regions(this Floor actual) => Assert.NotNull(actual.Regions);
}
