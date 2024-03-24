namespace GuildWars2.Pve.Dungeons;

/// <summary>Information about a path of a dungeon.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record DungeonPath
{
    /// <summary>The path ID.</summary>
    public required string Id { get; init; }

    /// <summary>The kind of path (stoy or explorable).</summary>
    public required DungeonKind Kind { get; init; }
}
