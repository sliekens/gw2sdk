using GW2SDK.Legends;
using Xunit;

namespace GW2SDK.Tests.Features.Legends;

public static class Invariants
{
    internal static void Has_id(this Legend actual) => Assert.NotEmpty(actual.Id);

    internal static void Has_code(this Legend actual) => Assert.True(actual.Code > 0);
}
