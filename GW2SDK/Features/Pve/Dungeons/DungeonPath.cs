namespace GuildWars2.Pve.Dungeons;

[PublicAPI]
[DataTransferObject]
public sealed record DungeonPath
{
    public required string Id { get; init; }

    public required DungeonKind Kind { get; init; }
}
