namespace GuildWars2.Raids;

[PublicAPI]
[DataTransferObject]
public sealed record Raid
{
    public required string Id { get; init; }

    public required IReadOnlyCollection<RaidWing> Wings { get; init; }
}
