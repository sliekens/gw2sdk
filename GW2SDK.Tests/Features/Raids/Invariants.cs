using GW2SDK.Raids;
using Xunit;

namespace GW2SDK.Tests.Features.Raids;

internal static class Invariants
{
    internal static void Has_id(this Raid actual) => Assert.NotEmpty(actual.Id);
}
