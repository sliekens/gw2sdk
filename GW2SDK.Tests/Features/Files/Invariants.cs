using GW2SDK.Files;
using Xunit;

namespace GW2SDK.Tests.Features.Files;

internal static class Invariants
{
    internal static void Has_id(this File actual) => Assert.NotEmpty(actual.Id);

    internal static void Has_icon(this File actual) => Assert.NotEmpty(actual.Icon);
}
