using GuildWars2.Skins;

namespace GuildWars2.Tests.Features.Skins;

internal static class Invariants
{
    internal static void Id_is_positive(this Skin actual) =>
        Assert.InRange(actual.Id, 1, int.MaxValue);
}
