using GW2SDK.Pvp.Amulets;
using Xunit;

namespace GW2SDK.Tests.Features.Pvp.Amulets;

internal static class Invariants
{
    internal static void Has_id(this Amulet actual) => Assert.True(actual.Id > 0);

    internal static void Has_name(this Amulet actual) => Assert.NotEmpty(actual.Name);

    internal static void Has_icon(this Amulet actual) => Assert.NotEmpty(actual.Icon);

    internal static void Has_attributes(this Amulet actual) => Assert.NotEmpty(actual.Attributes);
}
