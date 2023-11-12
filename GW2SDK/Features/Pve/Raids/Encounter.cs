namespace GuildWars2.Pve.Raids;

[PublicAPI]
[DataTransferObject]
public sealed record Encounter
{
    public required string Id { get; init; }

    public required EncounterKind Kind { get; init; }
}
