namespace GuildWars2.Pve.SuperAdventureBox;

/// <summary>Information about the Super Adventure Box progress of a character.</summary>
[DataTransferObject]
public sealed record SuperAdventureBoxProgress
{
    /// <summary>The zones which have been completed by the character.</summary>
    public required IReadOnlyCollection<SuperAdventureBoxZone> Zones { get; init; }

    /// <summary>The upgrades which have been unlocked by the character.</summary>
    public required IReadOnlyCollection<SuperAdventureBoxUpgrade> Unlocks { get; init; }

    /// <summary>The songs which have been unlocked by the character.</summary>
    public required IReadOnlyCollection<SuperAdventureBoxSong> Songs { get; init; }
}
