using GuildWars2.Exploration.Regions;

namespace GuildWars2.Tests.Features.Exploration.Regions;

public static class Invariants
{
    internal static void Has_id(this Region actual) => Assert.True(actual.Id > 0);

    internal static void Has_name(this Region actual) => Assert.NotEmpty(actual.Name);

    internal static void Has_maps(this Region actual) => Assert.NotEmpty(actual.Maps);
}
