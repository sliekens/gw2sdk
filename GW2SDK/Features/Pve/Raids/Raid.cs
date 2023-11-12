namespace GuildWars2.Pve.Raids;

[PublicAPI]
[DataTransferObject]
public sealed record Raid
{
    public required string Id { get; init; }

    public required IReadOnlyList<RaidWing> Wings { get; init; }
}
