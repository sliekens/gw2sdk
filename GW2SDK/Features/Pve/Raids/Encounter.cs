namespace GuildWars2.Pve.Raids;

/// <summary>Information about a raid encounter.</summary>
[DataTransferObject]
public sealed record Encounter
{
    /// <summary>The encounter ID.</summary>
    public required string Id { get; init; }

    /// <summary>The kind of encounter: checkpoint or boss.</summary>
    public required Extensible<EncounterKind> Kind { get; init; }
}
