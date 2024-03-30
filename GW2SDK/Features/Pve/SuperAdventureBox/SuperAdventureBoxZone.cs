namespace GuildWars2.Pve.SuperAdventureBox;

/// <summary>Information about a Super Adventure Box zone.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record SuperAdventureBoxZone
{
    /// <summary>The zone ID.</summary>
    public required int Id { get; init; }

    /// <summary>The difficulty of the zone.</summary>
    public required Extensible<SuperAdventureBoxMode> Mode { get; init; }

    /// <summary>The world number.</summary>
    public required int World { get; init; }

    /// <summary>The zone number.</summary>
    public required int Zone { get; init; }
}
