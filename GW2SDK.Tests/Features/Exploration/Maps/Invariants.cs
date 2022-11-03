using GW2SDK.Exploration.Maps;
using Xunit;

namespace GW2SDK.Tests.Features.Exploration.Maps;

public static class Invariants
{
    internal static void Has_id(this Map actual) => Assert.True(actual.Id > 0);

    internal static void Has_name(this Map actual) => Assert.NotEmpty(actual.Name);
}
