using GW2SDK.Exploration.Charts;
using Xunit;

namespace GW2SDK.Tests.Features.Exploration.Charts;

public static class Invariants
{
    internal static void Has_id(this Chart actual) => Assert.True(actual.Id > 0);

    internal static void Has_name(this Chart actual) => Assert.NotEmpty(actual.Name);
}
