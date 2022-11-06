using GW2SDK.Outfits;
using Xunit;

namespace GW2SDK.Tests.Features.Outfits;

internal static class Invariants
{
    internal static void Has_id(this Outfit actual) => Assert.True(actual.Id > 0);

    internal static void Has_name(this Outfit actual) => Assert.NotEmpty(actual.Name);

    internal static void Has_icon(this Outfit actual) => Assert.NotEmpty(actual.Icon);

    internal static void Has_unlock_items(this Outfit actual) =>
        Assert.NotEmpty(actual.UnlockItems);
}
