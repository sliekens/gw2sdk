namespace GuildWars2.Pve.Dungeons;

[PublicAPI]
[DataTransferObject]
public sealed record Dungeon
{
    public required string Id { get; init; }

    public required IReadOnlyCollection<DungeonPath> Paths { get; init; }
}
