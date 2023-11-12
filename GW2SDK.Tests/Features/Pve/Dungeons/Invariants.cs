using GuildWars2.Pve.Dungeons;

namespace GuildWars2.Tests.Features.Pve.Dungeons;

internal static class Invariants
{
    internal static void Has_id(this Dungeon actual) => Assert.NotEmpty(actual.Id);

    internal static void Has_paths(this Dungeon actual) => Assert.NotEmpty(actual.Paths);

    internal static void Has_id(this DungeonPath actual) => Assert.NotEmpty(actual.Id);

    internal static void Has_kind(this DungeonPath actual) =>
        Assert.True(Enum.IsDefined(typeof(DungeonKind), actual.Kind));
}
