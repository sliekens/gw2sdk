using System;
using GuildWars2.Worlds;
using Xunit;

namespace GuildWars2.Tests.Features.Worlds;

public static class Invariants
{
    public static void Id_is_positive(this World actual) =>
        Assert.InRange(actual.Id, 1, int.MaxValue);

    public static void Name_is_not_empty(this World actual) => Assert.NotEmpty(actual.Name);

    public static void World_population_type_is_supported(this World actual) =>
        Assert.True(Enum.IsDefined(typeof(WorldPopulation), actual.Population));
}
