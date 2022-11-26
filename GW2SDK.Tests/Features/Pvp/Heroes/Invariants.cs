using GW2SDK.Pvp.Heroes;
using Xunit;

namespace GW2SDK.Tests.Features.Pvp.Heroes;

internal static class Invariants
{
    internal static void Has_id(this Hero actual) => Assert.NotEmpty(actual.Id);

    internal static void Has_name(this Hero actual) => Assert.NotEmpty(actual.Name);
}
