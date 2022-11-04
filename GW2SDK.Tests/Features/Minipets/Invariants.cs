using GW2SDK.Minipets;
using Xunit;

namespace GW2SDK.Tests.Features.Minipets;

internal static class Invariants
{
    internal static void Has_id(this Minipet actual) => Assert.True(actual.Id > 0);

    internal static void Has_name(this Minipet actual) => Assert.NotEmpty(actual.Name);

    internal static void Has_icon(this Minipet actual) => Assert.NotEmpty(actual.Icon);

    internal static void Has_order(this Minipet actual) => Assert.True(actual.Order >= 0);

    internal static void Has_item_id(this Minipet actual) => Assert.True(actual.ItemId >= 0);
}
