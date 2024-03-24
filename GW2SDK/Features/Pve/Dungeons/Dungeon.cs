namespace GuildWars2.Pve.Dungeons;

/// <summary>Information about a dungeon, which is instanced PvE content designed for parties of five players.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Dungeon
{
    /// <summary>The dungeon ID.</summary>
    public required string Id { get; init; }

    /// <summary>The paths of the dungeon which can be selected at the start of the dungeon.</summary>
    public required IReadOnlyCollection<DungeonPath> Paths { get; init; }
}
