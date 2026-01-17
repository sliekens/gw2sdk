namespace GuildWars2.Pve.Raids;

/// <summary>Information about a wing of a raid.</summary>
[DataTransferObject]
public sealed record RaidWing
{
    /// <summary>The wing ID.</summary>
    public required string Id { get; init; }

    /// <summary>The encounters in this wing.</summary>
    public required IImmutableValueList<Encounter> Encounters { get; init; }
}
