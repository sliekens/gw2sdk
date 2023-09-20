namespace GuildWars2.Raids;

[PublicAPI]
[DataTransferObject]
public sealed record RaidWing
{
    public required string Id { get; init; }

    public required IReadOnlyCollection<Encounter> Encounters { get; init; }
}
