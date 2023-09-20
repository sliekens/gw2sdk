using System.Drawing;
using GuildWars2.Exploration.Continents;

namespace GuildWars2.Tests.Features.Exploration.Continents;

public static class Invariants
{
    internal static void Has_id(this Continent actual) => Assert.True(actual.Id > 0);

    internal static void Has_name(this Continent actual) => Assert.NotEmpty(actual.Name);

    internal static void Has_dimensions(this Continent actual) =>
        Assert.NotEqual(Size.Empty, actual.ContinentDimensions);

    internal static void Has_min_zoom(this Continent actual) =>
        Assert.True(actual.MinZoom >= 0 && actual.MinZoom < actual.MaxZoom);

    internal static void Has_max_zoom(this Continent actual) =>
        Assert.True(actual.MaxZoom > actual.MinZoom);

    internal static void Has_floors(this Continent actual) => Assert.NotEmpty(actual.Floors);
}
