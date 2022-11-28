using GuildWars2.Exploration.Maps;
using Xunit;

namespace GuildWars2.Tests.Features.Exploration.Maps;

public static class Invariants
{
    internal static void Has_id(this Map actual) => Assert.True(actual.Id > 0);

    internal static void Has_name(this Map actual) => Assert.NotEmpty(actual.Name);
}
