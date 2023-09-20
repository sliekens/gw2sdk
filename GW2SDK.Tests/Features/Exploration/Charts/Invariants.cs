using GuildWars2.Exploration.Charts;

namespace GuildWars2.Tests.Features.Exploration.Charts;

public static class Invariants
{
    internal static void Has_id(this Chart actual) => Assert.True(actual.Id > 0);

    internal static void Has_name(this Chart actual) => Assert.NotEmpty(actual.Name);
}
