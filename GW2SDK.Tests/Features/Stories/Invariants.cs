using GW2SDK.Stories;
using Xunit;

namespace GW2SDK.Tests.Features.Stories;

internal static class Invariants
{
    internal static void Has_id(this Story actual) => Assert.True(actual.Id > 0);

    internal static void Has_id(this Season actual) => Assert.NotEmpty(actual.Id);
}
